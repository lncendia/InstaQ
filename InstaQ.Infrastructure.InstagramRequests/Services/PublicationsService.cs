using InstaQ.Application.Abstractions.InstagramRequests.DTOs;
using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Exceptions;
using InstaQ.Infrastructure.InstagramRequests.Models;

namespace InstaQ.Infrastructure.InstagramRequests.Services;

public class PublicationsService : IPublicationsService
{
    private readonly IRequestSender _requestSender;
    private readonly IResponseHandler<(List<PublicationModel>, string?)> _handler;
    private readonly IResponseHandler<ErrorModel> _errorHandler;
    private readonly IResponseHandler<HashtagModel> _hashtagHandler;

    public PublicationsService(IRequestSender requestSender,
        IResponseHandler<(List<PublicationModel>, string?)> handler, IResponseHandler<HashtagModel> hashtagHandler,
        IResponseHandler<ErrorModel> errorHandler)
    {
        _requestSender = requestSender;
        _handler = handler;
        _errorHandler = errorHandler;
        _hashtagHandler = hashtagHandler;
    }

    private async Task<(List<PublicationModel>, int)> GetPublicationsAsync(string query, int count,
        string? nextFrom, CancellationToken token)
    {
        var countRequests = 0;
        var items = new List<PublicationModel>();
        do
        {
            var countRequest = count - items.Count;
            if (countRequest > 100) countRequest = 100;

            var queryParams = new Dictionary<string, string?>
                { { "name", query }, { "max_amount", countRequest.ToString() }, { "max_id", nextFrom } };
            var response = await _requestSender.SendAsync("v1/hashtag/medias/recent/chunk", queryParams, token);
            var data = _handler.MapResponse(response);
            items.AddRange(data.Item1);
            nextFrom = nextFrom != data.Item2 ? data.Item2 : null;
            countRequests++;
        } while (!string.IsNullOrEmpty(nextFrom) && items.Count < count);

        return (items, countRequests);
    }

    private async Task<HashtagModel> GetHashtagAsync(string name, CancellationToken token)
    {
        var queryParams = new Dictionary<string, string?> { { "name", name } };
        var response = await _requestSender.SendAsync("/a1/hashtag", queryParams, token);
        return _hashtagHandler.MapResponse(response);
    }


    public async Task<PublicationsResultDto> GetAsync(string hashtag, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less or equal zero.");
        if (hashtag.StartsWith('#')) hashtag = hashtag[1..];
        hashtag = hashtag.ToLower();
        var countRequests = 0;
        var publications = new List<PublicationDto>();
        try
        {
            var tag = await GetHashtagAsync(hashtag, token);
            var firstPublications = tag.Recent.Sections.SelectMany(x => x.Content.Medias).ToList();
            publications.AddRange(firstPublications.Select(item => new PublicationDto(item.Pk, item.User.Pk, item.Code,
                item.LikeCount, item.CommentCount, false)));
            countRequests++;
            if (publications.Count < count && tag.Recent.MoreAvailable)
            {
                var allPublications =
                    await GetPublicationsAsync(hashtag, count, tag.Recent.NextMaxId, token);
                countRequests += allPublications.Item2;
                publications.AddRange(allPublications.Item1.DistinctBy(x => x.Pk)
                    .Select(item => new PublicationDto(item.Pk, item.User.Pk, item.Code, item.LikeCount,
                        item.CommentCount, item.CommentsDisabled)));
            }
        }
        catch (RequestException ex)
        {
            if (ex.ResponseCode == 404) return new PublicationsResultDto(new List<PublicationDto>(), 1);
            if (string.IsNullOrEmpty(ex.Content)) throw new InstagramRequestException(ex.ResponseCode, null, ex);
            var error = _errorHandler.MapResponse(ex.Content);
            var message = error.Message;

            throw new InstagramRequestException(ex.ResponseCode, message, ex);
        }

        return new PublicationsResultDto(publications, countRequests);
    }
}
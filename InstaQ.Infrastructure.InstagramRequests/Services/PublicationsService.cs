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

    public PublicationsService(IRequestSender requestSender,
        IResponseHandler<(List<PublicationModel>, string?)> handler, IResponseHandler<ErrorModel> errorHandler)
    {
        _requestSender = requestSender;
        _handler = handler;
        _errorHandler = errorHandler;
    }

    private async Task<int> GetPublicationsPageAsync(List<PublicationModel> items, string query, int count,
        CancellationToken token)
    {
        int countRequests = 0;
        string? nextFrom = null;
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

        return countRequests;
    }


    public async Task<PublicationsResultDto> GetAsync(string hashtag, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var publications = new List<PublicationModel>();
        try
        {
            var countRequests = await GetPublicationsPageAsync(publications, hashtag.ToLower(), count, token);
            var list = publications.DistinctBy(x => x.Pk)
                .Select(item => new PublicationDto(item.Pk, item.User.Pk, item.Code, item.LikeCount, item.CommentCount, item.CommentsDisabled)).ToList();
            return new PublicationsResultDto(list, countRequests);
        }
        catch (RequestException ex)
        {
            if (ex.ResponseCode == 404) return new PublicationsResultDto(new List<PublicationDto>(), 1);
            if (string.IsNullOrEmpty(ex.Content)) throw new InstagramRequestException(ex.ResponseCode, null, ex);
            var error = _errorHandler.MapResponse(ex.Content);
            var message = error.Message;

            throw new InstagramRequestException(ex.ResponseCode, message, ex);
        }
    }
}
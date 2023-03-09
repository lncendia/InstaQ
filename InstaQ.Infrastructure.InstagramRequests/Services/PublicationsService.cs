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

    private async Task GetPublicationsPageAsync(List<PublicationModel> items, string query, int count, string? nextFrom,
        CancellationToken token)
    {
        if (items.Count >= count) return;
        var countRequest = count - items.Count;
        if (countRequest > 100) countRequest = 100;

        var queryParams = new Dictionary<string, string?>
            {{"name", query}, {"max_amount", countRequest.ToString()}, {"max_id", nextFrom}};
        var response = await _requestSender.SendAsync("v1/hashtag/medias/recent/chunk", queryParams, token);
        var data = _handler.MapResponse(response);
        items.AddRange(data.Item1);
        if (string.IsNullOrEmpty(data.Item2)) return;
        await GetPublicationsPageAsync(items, query, count, data.Item2, token);
    }


    public async Task<List<PublicationDto>> GetAsync(string hashtag, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var publications = new List<PublicationModel>();
        try
        {
            await GetPublicationsPageAsync(publications, hashtag, count, null, token);
            var list = publications.Select(item => new PublicationDto(item.Pk, item.User.Pk)).ToList();
            return list;
        }
        catch (RequestException ex)
        {
            if (string.IsNullOrEmpty(ex.Content)) throw new InstagramRequestException(ex.ResponseCode, null, ex);
            var error = _errorHandler.MapResponse(ex.Content);
            var message = error.Message;

            throw new InstagramRequestException(ex.ResponseCode, message, ex);
        }
    }
}
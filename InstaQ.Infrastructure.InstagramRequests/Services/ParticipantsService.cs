using InstaQ.Application.Abstractions.InstagramRequests.DTOs;
using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Exceptions;
using InstaQ.Infrastructure.InstagramRequests.Models;

namespace InstaQ.Infrastructure.InstagramRequests.Services;

public class ParticipantsService : IParticipantsService
{
    private readonly IRequestSender _requestSender;
    private readonly IResponseHandler<(List<ParticipantModel>, string?)> _handler;
    private readonly IResponseHandler<ErrorModel> _errorHandler;

    public ParticipantsService(IRequestSender requestSender,
        IResponseHandler<(List<ParticipantModel>, string?)> handler, IResponseHandler<ErrorModel> errorHandler)
    {
        _requestSender = requestSender;
        _handler = handler;
        _errorHandler = errorHandler;
    }

    private async Task GetFollowersPageAsync(List<ParticipantModel> items, string pk, int count, string? nextFrom,
        CancellationToken token)
    {
        if (items.Count >= count) return;
        var countRequest = count - items.Count;
        if (countRequest > 100) countRequest = 100;

        var queryParams = new Dictionary<string, string?>
            {{"user_id", pk}, {"amount", countRequest.ToString()}, {"max_id", nextFrom}};
        var response = await _requestSender.SendAsync("v1/user/followers/chunk", queryParams, token);
        var data = _handler.MapResponse(response);
        items.AddRange(data.Item1);
        if (string.IsNullOrEmpty(data.Item2)) return;
        await GetFollowersPageAsync(items, pk, count, data.Item2, token);
    }

    private async Task GetFollowingsPageAsync(List<ParticipantModel> items, string pk, int count,
        string? nextFrom, CancellationToken token)
    {
        if (items.Count >= count) return;
        var countRequest = count - items.Count;
        if (countRequest > 100) countRequest = 100;

        var queryParams = new Dictionary<string, string?>
            {{"user_id", pk}, {"amount", countRequest.ToString()}, {"end_cursor", nextFrom}};
        var response = await _requestSender.SendAsync("gql/user/following/chunk", queryParams, token);
        var data = _handler.MapResponse(response);
        items.AddRange(data.Item1);
        if (string.IsNullOrEmpty(data.Item2)) return;
        await GetFollowingsPageAsync(items, pk, count, data.Item2, token);
    }


    public async Task<List<ParticipantDto>> GetFollowersAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var participants = new List<ParticipantModel>();
        try
        {
            await GetFollowersPageAsync(participants, id, count, null, token);
            var list = participants.Select(item => new ParticipantDto(item.Pk, item.Username)).ToList();
            return list;
        }
        catch (RequestException ex)
        {
            throw HandleError(ex);
        }
    }

    public async Task<List<ParticipantDto>> GetFollowingsAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var participants = new List<ParticipantModel>();
        try
        {
            await GetFollowingsPageAsync(participants, id, count, null, token);
            var list = participants.Select(item => new ParticipantDto(item.Pk, item.Username)).ToList();
            return list;
        }
        catch (RequestException ex)
        {
            throw HandleError(ex);
        }
    }

    private Exception HandleError(RequestException ex)
    {
        if (string.IsNullOrEmpty(ex.Content)) return new InstagramRequestException(ex.ResponseCode, null, ex);
        var error = _errorHandler.MapResponse(ex.Content);
        var message = error.Message;

        return new InstagramRequestException(ex.ResponseCode, message, ex);
    }
}
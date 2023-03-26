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

    private async Task<(List<ParticipantModel>, int)> GetFollowersPageAsync(string pk, int count, CancellationToken token)
    {
        int countRequests = 0;
        var items = new List<ParticipantModel>();
        string? nextFrom = null;
        do
        {
            var countRequest = count - items.Count;
            if (countRequest > 46) countRequest = 46;

            var queryParams = new Dictionary<string, string?>
                { { "user_id", pk }, { "amount", countRequest.ToString() }, { "end_cursor", nextFrom } };
            var response = await _requestSender.SendAsync("gql/user/followers/chunk", queryParams, token);
            var data = _handler.MapResponse(response);
            items.AddRange(data.Item1);
            nextFrom = nextFrom != data.Item2 ? data.Item2 : null;
            countRequests++;
        } while (!string.IsNullOrEmpty(nextFrom) && items.Count < count);

        return (items, countRequests);
    }

    private async Task<int> GetFollowingsPageAsync(List<ParticipantModel> items, string pk, int count,
        CancellationToken token)
    {
        int countRequests = 0;
        string? nextFrom = null;
        do
        {
            var countRequest = count - items.Count;
            if (countRequest > 46) countRequest = 46;

            var queryParams = new Dictionary<string, string?>
                { { "user_id", pk }, { "amount", countRequest.ToString() }, { "end_cursor", nextFrom } };
            var response = await _requestSender.SendAsync("gql/user/following/chunk", queryParams, token);
            var data = _handler.MapResponse(response);
            items.AddRange(data.Item1);
            nextFrom = nextFrom != data.Item2 ? data.Item2 : null;
            countRequests++;
        } while (!string.IsNullOrEmpty(nextFrom) && items.Count < count);

        return countRequests;
    }


    public async Task<ParticipantsResultDto> GetFollowersAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");
        
        try
        {
            var participants = await GetFollowersPageAsync(id, count, token);
            var list = participants.Item1.Select(item => new ParticipantDto(item.Pk, item.Username)).ToList();
            return new ParticipantsResultDto(list, participants.Item2);
        }
        catch (RequestException ex)
        {
            if (ex.ResponseCode == 404) return new ParticipantsResultDto(new List<ParticipantDto>(), 1);
            throw HandleError(ex);
        }
    }

    public async Task<ParticipantsResultDto> GetFollowingsAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var participants = new List<ParticipantModel>();
        try
        {
            var countRequests = await GetFollowingsPageAsync(participants, id, count, token);
            var list = participants.Select(item => new ParticipantDto(item.Pk, item.Username)).ToList();
            return new ParticipantsResultDto(list, countRequests);
        }
        catch (RequestException ex)
        {
            if (ex.ResponseCode == 404) return new ParticipantsResultDto(new List<ParticipantDto>(), 1);
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
using InstaQ.Application.Abstractions.InstagramRequests.DTOs;
using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Exceptions;
using InstaQ.Infrastructure.InstagramRequests.Models;

namespace InstaQ.Infrastructure.InstagramRequests.Services;

public class LikesService : ILikesService
{
    private readonly IRequestSender _requestSender;
    private readonly IResponseHandler<(List<LikeModel>, string?)> _handler;
    private readonly IResponseHandler<ErrorModel> _errorHandler;

    public LikesService(IRequestSender requestSender,
        IResponseHandler<(List<LikeModel>, string?)> handler, IResponseHandler<ErrorModel> errorHandler)
    {
        _requestSender = requestSender;
        _handler = handler;
        _errorHandler = errorHandler;
    }

    private async Task<(List<LikeModel>, int)> LoadLikesAsync(string id, int count, CancellationToken token)
    {
        var countRequests = 0;
        var items = new List<LikeModel>();
        string? nextFrom = null;
        do
        {
            var queryParams = new Dictionary<string, string?> { { "media_id", id }, { "end_cursor", nextFrom } };
            var response = await _requestSender.SendAsync("gql/media/likers/chunk", queryParams, token);
            var data = _handler.MapResponse(response);
            items.AddRange(data.Item1);
            nextFrom = nextFrom != data.Item2 ? data.Item2 : null;
            countRequests++;
        } while (!string.IsNullOrEmpty(nextFrom) && items.Count < count);

        return (items, countRequests);
    }


    public async Task<LikesResultDto> GetAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        try
        {
            var likes = await LoadLikesAsync(id, count, token);
            return new LikesResultDto(likes.Item1.Select(x => x.Pk).ToList(), likes.Item2);
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
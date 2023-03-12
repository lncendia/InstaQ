using InstaQ.Application.Abstractions.InstagramRequests.DTOs;
using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Exceptions;
using InstaQ.Infrastructure.InstagramRequests.Models;

namespace InstaQ.Infrastructure.InstagramRequests.Services;

public class CommentsService : ICommentsService
{
    private readonly IRequestSender _requestSender;
    private readonly IResponseHandler<(List<CommentModel>, string?)> _handler;
    private readonly IResponseHandler<ErrorModel> _errorHandler;

    public CommentsService(IRequestSender requestSender,
        IResponseHandler<(List<CommentModel>, string?)> handler, IResponseHandler<ErrorModel> errorHandler)
    {
        _requestSender = requestSender;
        _handler = handler;
        _errorHandler = errorHandler;
    }

    private async Task<int> LoadCommentsAsync(List<CommentModel> items, string id, int count, CancellationToken token)
    {
        int countRequests = 0;
        string? nextFrom = null;
        do
        {
            var queryParams = new Dictionary<string, string?> { { "media_id", id }, { "end_cursor", nextFrom } };
            var response = await _requestSender.SendAsync("gql/comments/chunk", queryParams, token);
            var data = _handler.MapResponse(response);
            items.AddRange(data.Item1);
            nextFrom = nextFrom != data.Item2 ? data.Item2 : null;
            countRequests++;
        } while (!string.IsNullOrEmpty(nextFrom) && items.Count < count);

        return countRequests;
    }

    public async Task<CommentsResultDto> GetAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var comments = new List<CommentModel>();
        try
        {
            var countRequests = await LoadCommentsAsync(comments, id, count, token);
            return new CommentsResultDto(comments.Select(item => (item.Owner.Pk, item.Text)).ToList(), countRequests);
        }
        catch (RequestException ex)
        {
            if (ex.ResponseCode == 404) return new CommentsResultDto(new List<(string, string)>(), 1);
            if (string.IsNullOrEmpty(ex.Content)) throw new InstagramRequestException(ex.ResponseCode, null, ex);
            var error = _errorHandler.MapResponse(ex.Content);
            var message = error.Message;

            throw new InstagramRequestException(ex.ResponseCode, message, ex);
        }
    }
}
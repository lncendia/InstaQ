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

    private async Task GetCommentsPageAsync(List<CommentModel> items, string id, int count, string? nextFrom,
        CancellationToken token)
    {
        if (items.Count >= count) return;

        var queryParams = new Dictionary<string, string?> {{"media_id", id}, {"end_cursor", nextFrom}};
        var response = await _requestSender.SendAsync("gql/comments/chunk", queryParams, token);
        var data = _handler.MapResponse(response);
        items.AddRange(data.Item1);
        if (string.IsNullOrEmpty(data.Item2)) return;
        await GetCommentsPageAsync(items, id, count, data.Item2, token);
    }

    public async Task<List<(string id, string text)>> GetAsync(string id, int count, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");

        var comments = new List<CommentModel>();
        try
        {
            await GetCommentsPageAsync(comments, id, count, null, token);
            return comments.Select(item => (item.Owner.Pk, item.Text)).ToList();
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
using InstaQ.Application.Abstractions.InstagramRequests.DTOs;
using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Exceptions;
using InstaQ.Infrastructure.InstagramRequests.Models;

namespace InstaQ.Infrastructure.InstagramRequests.Services;

public class InstagramProfileService : IInstagramProfileService
{
    private readonly IRequestSender _requestSender;
    private readonly IResponseHandler<UserModel> _handler;
    private readonly IResponseHandler<ErrorModel> _errorHandler;

    public InstagramProfileService(IRequestSender requestSender, IResponseHandler<UserModel> handler,
        IResponseHandler<ErrorModel> errorHandler)
    {
        _requestSender = requestSender;
        _handler = handler;
        _errorHandler = errorHandler;
    }

    public async Task<ProfileDto> GetAsync(string username)
    {
        var queryParams = new Dictionary<string, string?> {{"username", username}};
        try
        {
            var response = await _requestSender.SendAsync("v1/user/by/username", queryParams, CancellationToken.None);
            var user = _handler.MapResponse(response);
            return new ProfileDto(user.Pk, user.FullName, user.IsPrivate, user.FollowersCount,
                user.FollowingsCount);
        }
        catch (RequestException ex)
        {
            if (ex.ResponseCode == 404) throw new ContentNotFoundException();
            if (string.IsNullOrEmpty(ex.Content)) throw new InstagramRequestException(ex.ResponseCode, null, ex);
            var error = _errorHandler.MapResponse(ex.Content);
            var message = error.Message;

            throw new InstagramRequestException(ex.ResponseCode, message, ex);
        }
    }
}
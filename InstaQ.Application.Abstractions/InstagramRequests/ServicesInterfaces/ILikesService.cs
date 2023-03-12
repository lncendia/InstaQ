using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface ILikesService
{
    Task<LikesResultDto> GetAsync(string id, int count, CancellationToken token);
}
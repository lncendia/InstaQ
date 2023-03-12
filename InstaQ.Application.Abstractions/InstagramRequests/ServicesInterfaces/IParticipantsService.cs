using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface IParticipantsService
{
    Task<ParticipantsResultDto> GetFollowersAsync(string id, int count, CancellationToken token);
    Task<ParticipantsResultDto> GetFollowingsAsync(string id, int count, CancellationToken token);
}
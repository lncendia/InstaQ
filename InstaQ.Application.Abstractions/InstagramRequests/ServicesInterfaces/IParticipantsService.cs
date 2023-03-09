using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface IParticipantsService
{
    Task<List<ParticipantDto>> GetFollowersAsync(string id, int count, CancellationToken token);
    Task<List<ParticipantDto>> GetFollowingsAsync(string id, int count, CancellationToken token);
}
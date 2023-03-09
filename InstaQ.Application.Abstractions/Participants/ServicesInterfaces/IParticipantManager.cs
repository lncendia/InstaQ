using InstaQ.Application.Abstractions.Participants.DTOs;

namespace InstaQ.Application.Abstractions.Participants.ServicesInterfaces;

public interface IParticipantManager
{
    public Task<List<ParticipantDto>> FindAsync(Guid userId, SearchQuery query);
    public Task EditAsync(Guid userId, Guid participantId, Guid? parentId, string? note, bool vip);
    public Task DeleteAsync(Guid userId, Guid participantId);
    public Task<GetParticipantDto> GetAsync(Guid userId, Guid participantId);
}
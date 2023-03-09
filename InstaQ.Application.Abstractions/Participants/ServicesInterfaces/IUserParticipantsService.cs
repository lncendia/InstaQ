namespace InstaQ.Application.Abstractions.Participants.ServicesInterfaces;

public interface IUserParticipantsService
{
    Task<List<(Guid id, string name)>> GetUserParticipantsAsync(Guid userId);
}
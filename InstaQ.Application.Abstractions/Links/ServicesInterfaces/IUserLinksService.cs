namespace InstaQ.Application.Abstractions.Links.ServicesInterfaces;

public interface IUserLinksService
{
    Task<List<(Guid id, string name)>> GetUserLinksAsync(Guid userId);
}
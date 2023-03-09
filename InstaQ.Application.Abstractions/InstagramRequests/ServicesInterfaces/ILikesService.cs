namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface ILikesService
{
    Task<List<string>> GetAsync(string id, int count, CancellationToken token);
}
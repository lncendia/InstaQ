namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface ICommentsService
{
    Task<List<(string id, string text)>> GetAsync(string id, int count, CancellationToken token);
}
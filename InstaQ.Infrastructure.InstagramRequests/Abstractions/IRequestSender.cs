namespace InstaQ.Infrastructure.InstagramRequests.Abstractions;

public interface IRequestSender
{
    public Task<string> SendAsync(string name, Dictionary<string, string?> queryParams, CancellationToken token);
}
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Exceptions;
using RestSharp;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class RequestSender : IRequestSender
{
    private readonly string _token;

    public RequestSender(string token) => _token = token;

    public async Task<string> SendAsync(string name, Dictionary<string, string?> queryParams, CancellationToken token)
    {
        var client = new RestClient("https://api.lamadava.com/");
        var request = new RestRequest(name);
        request.AddHeader("x-access-key", _token);
        foreach (var keyValuePair in queryParams)
        {
            request.AddQueryParameter(keyValuePair.Key, keyValuePair.Value);
        }

        var response = await client.GetAsync(request, token);
        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            throw new RequestException((int) response.StatusCode, response.Content);
        return response.Content!;
    }
}
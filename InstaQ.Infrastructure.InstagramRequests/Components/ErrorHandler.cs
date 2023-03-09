using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class ErrorHandler : IResponseHandler<ErrorModel>
{
    public ErrorModel MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<ErrorModel>(data)!;
    }
}
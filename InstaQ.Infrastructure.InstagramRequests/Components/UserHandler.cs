using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class UserHandler : IResponseHandler<UserModel>
{
    public UserModel MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<UserModel>(data)!;
    }
}
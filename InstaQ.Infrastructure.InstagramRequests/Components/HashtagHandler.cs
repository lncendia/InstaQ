using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Converters;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class HashtagHandler : IResponseHandler<HashtagModel>
{
    public HashtagModel MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<HashtagModel>(data, new HashtagResponseConverter(), new MediaResponseConverter())!;
    }
}
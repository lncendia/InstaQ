using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Converters;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class LikesHandler : IResponseHandler<(List<LikeModel> model, string? next)>
{
    public (List<LikeModel> model, string? next) MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<(List<LikeModel> model, string? next)>(data,
            new LamadavaResponseConverter<LikeModel>());
    }
}
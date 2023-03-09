using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Converters;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class CommentsHandler : IResponseHandler<(List<CommentModel> model, string? next)>
{
    public (List<CommentModel> model, string? next) MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<(List<CommentModel> model, string? next)>(data,
            new LamadavaResponseConverter<CommentModel>())!;
    }
}
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Converters;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class PublicationsHandler : IResponseHandler<(List<PublicationModel> model, string? next)>
{
    public (List<PublicationModel> model, string? next) MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<(List<PublicationModel> model, string? next)>(data,
            new LamadavaResponseConverter<PublicationModel>());
    }
}
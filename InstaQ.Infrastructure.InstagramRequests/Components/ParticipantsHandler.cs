using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Converters;
using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Components;

public class ParticipantsHandler : IResponseHandler<(List<ParticipantModel> model, string? next)>
{
    public (List<ParticipantModel> model, string? next) MapResponse(string data)
    {
        return JsonConvert.DeserializeObject<(List<ParticipantModel> model, string? next)>(data,
            new LamadavaResponseConverter<ParticipantModel>())!;
    }
}
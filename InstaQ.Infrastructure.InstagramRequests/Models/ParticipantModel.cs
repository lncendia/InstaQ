using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class ParticipantModel
{
    [JsonProperty("pk")] public string Pk { get; set; } = null!;
    [JsonProperty("username")] public string Username { get; set; } = null!;
}
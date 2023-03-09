using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class LikeModel
{
    [JsonProperty("id")] public string Pk { get; set; } = null!;
}
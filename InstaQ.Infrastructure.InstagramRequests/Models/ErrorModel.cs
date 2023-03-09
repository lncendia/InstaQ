using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class ErrorModel
{
    [JsonProperty("detail")] public string Message { get; set; } = null!;
    [JsonProperty("exc_type")] public string Type { get; set; } = null!;
}
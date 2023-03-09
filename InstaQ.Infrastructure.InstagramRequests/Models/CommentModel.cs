using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class CommentModel
{
    [JsonProperty("text")] public string Text { get; set; } = null!;
    [JsonProperty("owner")]public Owner Owner { get; set; } = null!;
}

public class Owner
{
    [JsonProperty("id")] public string Pk { get; set; } = null!;
}
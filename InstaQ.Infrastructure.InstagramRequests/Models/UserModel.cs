using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class UserModel
{
    [JsonProperty("pk")] public string Pk { get; set; } = null!;
    [JsonProperty("username")] public string Username { get; set; } = null!;
    [JsonProperty("full_name")] public string FullName { get; set; } = null!;
    [JsonProperty("is_private")] public bool IsPrivate { get; set; }
    [JsonProperty("follower_count")] public int FollowersCount { get; set; }
    [JsonProperty("following_count")] public int FollowingsCount { get; set; }
}
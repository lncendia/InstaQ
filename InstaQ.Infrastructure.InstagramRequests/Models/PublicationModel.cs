using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class PublicationModel
{
    [JsonProperty("pk")] public string Pk { get; set; } = null!;
    [JsonProperty("id")] public string Id { get; set; } = null!;
    [JsonProperty("thumbnail_url")] public string? ThumbnailUrl { get; set; }
    [JsonProperty("user")] public User User { get; set; } = null!;
    [JsonProperty("comment_count")] public long CommentCount { get; set; }
    [JsonProperty("like_count")] public long LikeCount { get; set; }
    [JsonProperty("comments_disabled")] public bool CommentsDisabled { get; set; }
}

public class User
{
    [JsonProperty("pk")] public string Pk { get; set; } = null!;
}
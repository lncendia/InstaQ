using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Models;

public class HashtagModel
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = null!;
    [JsonProperty("media_count")] public int MediaCount { get; set; }
    [JsonProperty("recent")] public MediaPlot Recent { get; set; } = null!;
}

public class MediaModel
{
    [JsonProperty("pk")] public string Pk { get; set; } = null!;
    [JsonProperty("code")] public string Code { get; set; } = null!;
    [JsonProperty("user")] public OwnerModel User { get; set; } = null!;
    [JsonProperty("comment_count")] public long CommentCount { get; set; }
    [JsonProperty("like_count")] public long LikeCount { get; set; }
}

public class OwnerModel
{
    [JsonProperty("pk")] public string Pk { get; set; } = null!;
}

public class SectionModel
{
    [JsonProperty("layout_content")] public LayoutContent Content { get; set; } = null!;
}

public class LayoutContent
{
    [JsonProperty("medias")] public List<MediaModel> Medias { get; set; } = null!;
}

public class MediaPlot
{
    [JsonProperty("more_available")] public bool MoreAvailable { get; set; }
    [JsonProperty("next_max_id")] public string? NextMaxId { get; set; }
    [JsonProperty("sections")] public List<SectionModel> Sections { get; set; } = null!;
}
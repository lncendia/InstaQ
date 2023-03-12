namespace InstaQ.Domain.Reposts.LikeReport.DTOs;

public class LikesDto
{
    public LikesDto(string pk, IReadOnlyCollection<string> likes)
    {
        Likes = likes;
        SuccessLoaded = true;
        Pk = pk;
    }
    public LikesDto(string pk)
    {
        Pk = pk;
        SuccessLoaded = false;
    }

    public string Pk { get; }
    public IReadOnlyCollection<string>? Likes { get; }
    public bool SuccessLoaded { get; }
}
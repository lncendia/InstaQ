namespace InstaQ.Domain.Reposts.LikeReport.DTOs;

public class LikesDto
{
    public LikesDto(string publicationId, string pk, IReadOnlyCollection<string> likes)
    {
        PublicationId = publicationId;
        Likes = likes;
        SuccessLoaded = true;
        Pk = pk;
    }
    public LikesDto(string publicationId, string pk)
    {
        PublicationId = publicationId;
        Pk = pk;
        SuccessLoaded = false;
    }

    public string Pk { get; }
    public string PublicationId { get; }
    public IReadOnlyCollection<string>? Likes { get; }
    public bool SuccessLoaded { get; }
}
//todo: check this for use
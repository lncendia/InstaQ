namespace InstaQ.Domain.Reposts.CommentReport.DTOs;

public class CommentsDto
{
    public CommentsDto(string publicationId, string pk, IReadOnlyCollection<(string, string)> comments)
    {
        PublicationId = publicationId;
        Comments = comments;
        SuccessLoaded = true;
        OwnerId = pk;
    }

    public CommentsDto(string publicationId, string pk)
    {
        PublicationId = publicationId;
        OwnerId = pk;
        SuccessLoaded = false;
    }

    public string OwnerId { get; }
    public string PublicationId { get; }
    public IReadOnlyCollection<(string, string)>? Comments { get; }
    public bool SuccessLoaded { get; }
}
namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class CommentsResultDto
{
    public CommentsResultDto(long ownerId, long publicationId, List<(long, string)> comments)
    {
        PublicationId = publicationId;
        Comments = comments;
        OwnerId = ownerId;
    }

    public long OwnerId { get; }
    public long PublicationId { get; }
    public List<(long, string)> Comments { get; }
}
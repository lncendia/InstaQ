namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class RepostsDto
{
    public RepostsDto(long publicationId, long ownerId, List<long> reposts)
    {
        PublicationId = publicationId;
        Reposts = reposts;
        OwnerId = ownerId;
    }

    public long OwnerId { get; }
    public long PublicationId { get; }
    public List<long> Reposts { get; }
}
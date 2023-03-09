namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class LikesResultDto
{
    public LikesResultDto(long publicationId, long ownerId, List<long> likes)
    {
        PublicationId = publicationId;
        Likes = likes;
        OwnerId = ownerId;
    }

    public long OwnerId { get; }
    public long PublicationId { get; }
    public List<long> Likes { get; }
}
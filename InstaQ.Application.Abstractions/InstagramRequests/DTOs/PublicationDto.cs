namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class PublicationDto
{
    public PublicationDto(string publicationId, string ownerId)
    {
        PublicationId = publicationId;
        OwnerId = ownerId;
    }

    public string PublicationId { get;  }
    public string OwnerId { get;  }
}
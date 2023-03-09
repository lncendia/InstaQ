namespace InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeDto
{
    public LikeDto(int publicationId, bool isConfirmed)
    {
        PublicationId = publicationId;
        IsConfirmed = isConfirmed;
    }

    public int PublicationId { get; }
    public bool IsConfirmed { get; }
}
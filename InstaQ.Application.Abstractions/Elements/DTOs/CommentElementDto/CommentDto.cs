namespace InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;

public class CommentDto
{
    public CommentDto(int publicationId, bool isConfirmed, string? text)
    {
        PublicationId = publicationId;
        IsConfirmed = isConfirmed;
        Text = text;
    }

    public int PublicationId { get; }
    public bool IsConfirmed { get; }
    public string? Text { get; }
}
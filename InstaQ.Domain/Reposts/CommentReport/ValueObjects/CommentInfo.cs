namespace InstaQ.Domain.Reposts.CommentReport.ValueObjects;

public class CommentInfo
{
    internal CommentInfo(int publicationId, bool isConfirmed, string? text)
    {
        if (!string.IsNullOrEmpty(text)) Text = text[..Math.Min(text.Length, 10)];
        PublicationId = publicationId;
        IsConfirmed = isConfirmed;
    }

    public int PublicationId { get; }
    public bool IsConfirmed { get; }
    public string? Text { get; }
}
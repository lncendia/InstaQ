namespace InstaQ.Domain.Reposts.LikeReport.ValueObjects;

public class LikeInfo
{
    internal LikeInfo(int publicationId, bool isConfirmed)
    {
        IsConfirmed = isConfirmed;
        PublicationId = publicationId;
    }
    
    public int PublicationId { get; }
    public bool IsConfirmed { get; }
}
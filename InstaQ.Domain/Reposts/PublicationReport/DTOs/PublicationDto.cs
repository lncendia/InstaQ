namespace InstaQ.Domain.Reposts.PublicationReport.DTOs;

public class PublicationDto
{
    public PublicationDto(string itemId, string pk)
    {
        ItemId = itemId;
        Pk = pk;
    }

    public string ItemId { get; }
    public string Pk { get; }
}
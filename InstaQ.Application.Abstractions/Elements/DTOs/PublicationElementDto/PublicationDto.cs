namespace InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

public class PublicationDto
{
    public PublicationDto(int id, string itemId, string pk, bool isLoaded)
    {
        Id = id;
        ItemId = itemId;
        Pk = pk;
        IsLoaded = isLoaded;
    }

    public int Id { get; }
    public string ItemId { get; }
    public string Pk { get; }
    public bool IsLoaded { get; }
}
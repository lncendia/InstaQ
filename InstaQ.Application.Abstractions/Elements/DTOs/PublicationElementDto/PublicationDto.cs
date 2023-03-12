namespace InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

public class PublicationDto
{
    public PublicationDto(int id, string ownerPk, string code, bool isLoaded)
    {
        Id = id;
        OwnerPk = ownerPk;
        IsLoaded = isLoaded;
        Code = code;
    }

    public int Id { get; }
    public string OwnerPk { get; }
    public string Code { get; }
    public bool IsLoaded { get; }
}
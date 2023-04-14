namespace InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;

public class ElementDto
{
    protected ElementDto(string name, string pk)
    {
        Name = name;
        Pk = pk;
    }
    
    public string Name { get; }
    public string Pk { get; }
}
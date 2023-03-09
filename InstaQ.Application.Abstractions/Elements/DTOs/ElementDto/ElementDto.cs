namespace InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;

public class ElementDto
{
    protected ElementDto(ElementBuilder builder)
    {
        Name = builder.Name ?? throw new ArgumentException("builder not formed", nameof(builder));
        Pk = builder.Pk ?? throw new ArgumentException("builder not formed", nameof(builder));
    }
    
    public string Name { get; }
    public string Pk { get; }
}
namespace InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;

public class ElementBuilder
{
    public string? Name { get; private set; }
    public string? Pk { get; private set; }

    public ElementBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public ElementBuilder WithPk(string pk)
    {
        Pk = pk;
        return this;
    }
}
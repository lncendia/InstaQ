using InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;
using InstaQ.Domain.Reposts.ParticipantReport.Enums;

namespace InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementBuilder : ElementBuilder
{
    public string? NewName { get; private set; }
    public ElementType? Type { get; private set; }
    public IEnumerable<ParticipantElementDto>? Children { get; private set; }

    private ParticipantElementBuilder()
    {
    }

    public static ParticipantElementBuilder ParticipantReportElementDto() => new();

    public ParticipantElementBuilder WithType(ElementType type, string? newName = null)
    {
        NewName = newName;
        Type = type;
        return this;
    }

    public ParticipantElementBuilder WithChildren(IEnumerable<ParticipantElementDto> children)
    {
        Children = children;
        return this;
    }

    public ParticipantElementDto Build() => new(this);
}
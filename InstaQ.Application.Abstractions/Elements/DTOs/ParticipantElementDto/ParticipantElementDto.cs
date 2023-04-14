using InstaQ.Domain.Reposts.ParticipantReport.Enums;

namespace InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementDto : ElementDto.ElementDto
{
    public ParticipantElementDto(string name, string pk, string? newName, ElementType? type, IEnumerable<ParticipantElementDto> children) : base(name, pk)
    {
        NewName = newName;
        Type = type;
        Children.AddRange(children);
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
    public List<ParticipantElementDto> Children { get; } = new();
}
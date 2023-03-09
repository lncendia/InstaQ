using InstaQ.Domain.Reposts.ParticipantReport.Enums;

namespace InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementDto : ElementDto.ElementDto
{
    public ParticipantElementDto(ParticipantElementBuilder builder) : base(builder)
    {
        Type = builder.Type;
        NewName = builder.NewName;
        if (builder.Children != null) Children.AddRange(builder.Children);
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
    public List<ParticipantElementDto> Children { get; } = new();
}
using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Elements;
using InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

namespace InstaQ.WEB.Mappers.ElementMapper;

public class ParticipantElementMapper : IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>
{
    public ParticipantElementViewModel Map(ParticipantElementDto element) => MapRecursion(element);

    private static ParticipantElementViewModel MapRecursion(ParticipantElementDto dto) =>
        new(dto.Name, dto.Pk, dto.NewName, dto.Type, dto.Children.Select(MapRecursion));
}
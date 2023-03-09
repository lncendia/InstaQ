using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Elements;
using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

namespace InstaQ.WEB.Mappers.ElementMapper;

public class LikeElementMapper : IElementMapperUnit<LikeElementDto, LikeElementViewModel>
{
    public LikeElementViewModel Map(LikeElementDto element) => MapRecursion(element);

    private static LikeElementViewModel MapRecursion(LikeElementDto dto) =>
        new(dto.Name, dto.Pk, dto.LikeChatName, dto.ParticipantId, dto.IsAccepted, dto.Vip, dto.Note,
            dto.Children.Select(MapRecursion), dto.Likes.Select(x => new LikeViewModel(x.PublicationId, x.IsConfirmed)));
}
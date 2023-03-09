using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Elements;
using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;

namespace InstaQ.WEB.Mappers.ElementMapper;

public class CommentElementMapper : IElementMapperUnit<CommentElementDto, CommentElementViewModel>
{
    public CommentElementViewModel Map(CommentElementDto element) => MapRecursion(element);

    private static CommentElementViewModel MapRecursion(CommentElementDto dto) =>
        new(dto.Name, dto.Pk, dto.LikeChatName, dto.ParticipantId, dto.IsAccepted, dto.Vip, dto.Note,
            dto.Children.Select(MapRecursion),
            dto.Comments.Select(x => new CommentViewModel(x.PublicationId, x.Text, x.IsConfirmed)));
}
using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Elements;
using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

namespace InstaQ.WEB.Mappers.ElementMapper;

public class ElementMapper : IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeElementViewModel>> LikeElementMapper =>
        new(() => new LikeElementMapper());

    public Lazy<IElementMapperUnit<CommentElementDto, CommentElementViewModel>> CommentElementMapper =>
        new(() => new CommentElementMapper());

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>> ParticipantElementMapper =>
        new(() => new ParticipantElementMapper());
}
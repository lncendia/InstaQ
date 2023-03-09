using InstaQ.WEB.ViewModels.Elements;
using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

namespace InstaQ.WEB.Mappers.Abstractions;

public interface IElementMapper
{
    Lazy<IElementMapperUnit<LikeElementDto, LikeElementViewModel>> LikeElementMapper { get; }
    Lazy<IElementMapperUnit<CommentElementDto, CommentElementViewModel>> CommentElementMapper { get; }

    Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>> ParticipantElementMapper { get; }
}
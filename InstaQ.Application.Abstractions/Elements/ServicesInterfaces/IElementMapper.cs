using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeReportElement>> LikeReportElementMapper { get; }
    public Lazy<IElementMapperUnit<CommentElementDto, CommentReportElement>> CommentReportElementMapper { get; }

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper { get; }
}
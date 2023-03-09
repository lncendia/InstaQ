using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using InstaQ.Application.Abstractions.Elements.ServicesInterfaces;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Services.Elements.Mappers;

public class ElementMapper : IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeReportElement>> LikeReportElementMapper =>
        new(() => new LikeReportElementMapper());

    public Lazy<IElementMapperUnit<CommentElementDto, CommentReportElement>> CommentReportElementMapper =>
        new(() => new CommentReportElementMapper());

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper => new(() => new ParticipantReportElementMapper());
}
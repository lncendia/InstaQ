using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Services.ReportsQuery.Mappers;

public class ReportMapper : IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReport>> LikeReportMapper => new(() => new LikeReportMapper());

    public Lazy<IReportMapperUnit<CommentReportDto, CommentReport>> CommentReportMapper =>
        new(() => new CommentReportMapper());

    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}
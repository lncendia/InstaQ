using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Reports;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

namespace InstaQ.WEB.Mappers.ReportMapper;

public class ReportMapper : IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReportViewModel>> LikeReportMapper =>
        new(() => new LikeReportMapper());

    public Lazy<IReportMapperUnit<CommentReportDto, CommentReportViewModel>> CommentReportMapper =>
        new(() => new CommentReportMapper());

    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());
}
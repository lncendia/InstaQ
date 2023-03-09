using InstaQ.WEB.ViewModels.Reports;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

namespace InstaQ.WEB.Mappers.Abstractions;

public interface IReportMapper
{
    Lazy<IReportMapperUnit<LikeReportDto, LikeReportViewModel>> LikeReportMapper { get; }
    Lazy<IReportMapperUnit<CommentReportDto, CommentReportViewModel>> CommentReportMapper { get; }
    Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>> ParticipantReportMapper { get; }
}
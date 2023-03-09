using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReport>> LikeReportMapper { get; }
    public Lazy<IReportMapperUnit<CommentReportDto, CommentReport>> CommentReportMapper { get; }
    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper { get; }
}
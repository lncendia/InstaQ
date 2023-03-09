using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportProcessorService
{
    Lazy<IReportProcessorUnit<LikeReport>> LikeReportProcessor { get; }
    Lazy<IReportProcessorUnit<CommentReport>> CommentReportProcessor { get; }
    Lazy<IReportProcessorUnit<ParticipantReport>> ParticipantReportProcessor { get; }
}
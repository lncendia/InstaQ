using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportInitializerService
{
    Lazy<IReportInitializerUnit<LikeReport>> LikeReportInitializer { get; }
    Lazy<IReportInitializerUnit<ParticipantReport>> ParticipantReportInitializer { get; }
    Lazy<IReportInitializerUnit<CommentReport>> CommentReportInitializer { get; }
}
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Services.ReportsProcessors.Initializers;

public class ReportInitializerService : IReportInitializerService
{
    public ReportInitializerService(IPublicationsService publicationGetterService, IUnitOfWork unitOfWork)
    {
        LikeReportInitializer = new Lazy<IReportInitializerUnit<LikeReport>>(() =>
            new LikeReportInitializer(unitOfWork, publicationGetterService));
        CommentReportInitializer = new Lazy<IReportInitializerUnit<CommentReport>>(() =>
            new CommentReportInitializer(unitOfWork, publicationGetterService));
        ParticipantReportInitializer =
            new Lazy<IReportInitializerUnit<ParticipantReport>>(() => new ParticipantReportInitializer(unitOfWork));
    }

    public Lazy<IReportInitializerUnit<LikeReport>> LikeReportInitializer { get; }

    public Lazy<IReportInitializerUnit<ParticipantReport>> ParticipantReportInitializer { get; }
    public Lazy<IReportInitializerUnit<CommentReport>> CommentReportInitializer { get; }
}
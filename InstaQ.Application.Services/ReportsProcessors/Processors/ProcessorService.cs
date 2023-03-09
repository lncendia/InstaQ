using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Services.ReportsProcessors.Processors;

public class ProcessorService : IReportProcessorService
{
    public ProcessorService(IUnitOfWork unitOfWork, ILikesService likeInfoService,
        ICommentsService commentInfoService, IParticipantsService participantsGetterService)
    {
        LikeReportProcessor = new Lazy<IReportProcessorUnit<LikeReport>>(
            () => new LikeReportProcessor(unitOfWork, likeInfoService));
        CommentReportProcessor = new Lazy<IReportProcessorUnit<CommentReport>>(
            () => new CommentReportProcessor(unitOfWork, commentInfoService));
        ParticipantReportProcessor = new Lazy<IReportProcessorUnit<ParticipantReport>>(
            () => new ParticipantReportProcessor(participantsGetterService));
    }

    public Lazy<IReportProcessorUnit<LikeReport>> LikeReportProcessor { get; }
    public Lazy<IReportProcessorUnit<CommentReport>> CommentReportProcessor { get; }
    public Lazy<IReportProcessorUnit<ParticipantReport>> ParticipantReportProcessor { get; }
}
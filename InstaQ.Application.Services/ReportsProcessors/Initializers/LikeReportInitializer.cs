using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Application.Services.ReportsProcessors.StaticMethods;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.LikeReport.Entities;

namespace InstaQ.Application.Services.ReportsProcessors.Initializers;

public class LikeReportInitializer : IReportInitializerUnit<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationsService _publicationGetterService;

    public LikeReportInitializer(IUnitOfWork unitOfWork, IPublicationsService publicationGetterService)
    {
        _unitOfWork = unitOfWork;
        _publicationGetterService = publicationGetterService;
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public async Task InitializeReportAsync(LikeReport report, CancellationToken token)
    {
        var t1 = Initializer.GetPublicationsAsync(report, _publicationGetterService, token);
        var t2 = Initializer.GetParticipantsAsync(report, _unitOfWork);
        await Task.WhenAll(t1, t2);
        report.Start(t2.Result, t1.Result);
    }
}
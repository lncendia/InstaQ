using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Participants.Specification;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Services.ReportsProcessors.Initializers;

public class ParticipantReportInitializer : IReportInitializerUnit<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantReportInitializer(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task InitializeReportAsync(ParticipantReport report, CancellationToken token)
    {
        var participants =
            await _unitOfWork.ParticipantRepository.Value.FindAsync(
                new ParticipantsByUserIdSpecification(report.UserId));
        report.Start(participants);
    }
}
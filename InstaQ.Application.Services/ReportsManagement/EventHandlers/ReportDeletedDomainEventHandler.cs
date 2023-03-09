using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.ReportLogs.Specification;
using InstaQ.Domain.Reposts.BaseReport.Events;
using MediatR;

namespace InstaQ.Application.Services.ReportsManagement.EventHandlers;

public class ReportDeletedDomainEventHandler : INotificationHandler<ReportDeletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportDeletedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ReportDeletedEvent notification, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.ReportLogRepository.Value.FindAsync(
            new LogByReportIdSpecification(notification.ReportId));
        foreach (var log in logs)
        {
            log.ReportDeleted();
            if (!log.IsFinished) log.Finish(false, DateTimeOffset.Now);

            await _unitOfWork.ReportLogRepository.Value.UpdateAsync(log);
        }
    }
}
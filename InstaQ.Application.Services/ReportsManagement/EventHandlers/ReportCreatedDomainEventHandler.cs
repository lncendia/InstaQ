using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.Reposts.BaseReport.Events;
using MediatR;

namespace InstaQ.Application.Services.ReportsManagement.EventHandlers;

public class ReportCreatedDomainEventHandler : INotificationHandler<ReportCreatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportCreatedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ReportCreatedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var reportLog in notification.Users.Select(user => new ReportLog(user, notification.ReportId,
                     notification.Type, notification.CreatedAt, notification.AdditionalInfo)))
        {
            await _unitOfWork.ReportLogRepository.Value.AddAsync(reportLog);
        }
    }
}
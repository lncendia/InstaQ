using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.BaseReport.Events;
using MediatR;

namespace InstaQ.Application.Services.Payments.EventHandlers;

public class BalanceWithdrawalCanceledReportDomainEventHandler : INotificationHandler<ReportDeletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly decimal _costOfRequest;

    public BalanceWithdrawalCanceledReportDomainEventHandler(IUnitOfWork unitOfWork, decimal costOfRequest)
    {
        _unitOfWork = unitOfWork;
        _costOfRequest = costOfRequest;
    }

    public async Task Handle(ReportDeletedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(notification.OwnerId);
        if (user is null) throw new UserNotFoundException();
        user.Balance -= notification.CountRequests * _costOfRequest;
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
    }
}
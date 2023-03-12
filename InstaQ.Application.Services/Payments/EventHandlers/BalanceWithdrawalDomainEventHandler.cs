using MediatR;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.BaseReport.Events;

namespace InstaQ.Application.Services.Payments.EventHandlers;

public class BalanceWithdrawalDomainEventHandler : INotificationHandler<ReportFinishedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly decimal _costOfRequest;

    public BalanceWithdrawalDomainEventHandler(IUnitOfWork unitOfWork, decimal costOfRequest)
    {
        _unitOfWork = unitOfWork;
        _costOfRequest = costOfRequest;
    }

    public async Task Handle(ReportFinishedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(notification.OwnerId);
        if (user is null) throw new UserNotFoundException();
        user.Balance -= notification.CountRequests * _costOfRequest;
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
    }
}
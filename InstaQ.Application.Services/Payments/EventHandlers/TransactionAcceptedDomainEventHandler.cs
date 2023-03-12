using MediatR;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Transactions.Events;

namespace InstaQ.Application.Services.Payments.EventHandlers;

public class TransactionAcceptedDomainEventHandler : INotificationHandler<TransactionAcceptedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionAcceptedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(TransactionAcceptedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(notification.UserId);
        if (user is null) throw new UserNotFoundException();
        user.Balance += notification.Amount;
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
    }
}
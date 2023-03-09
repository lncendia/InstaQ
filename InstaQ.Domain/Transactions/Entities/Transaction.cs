using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Transactions.Events;
using InstaQ.Domain.Transactions.Exceptions;

namespace InstaQ.Domain.Transactions.Entities;

public class Transaction : AggregateRoot
{
    public Transaction(string paymentSystemId, string paymentSystemUrl, decimal amount, Guid userId)
    {
        PaymentSystemId = paymentSystemId;
        PaymentSystemUrl = paymentSystemUrl;
        Amount = amount;
        UserId = userId;
    }


    public Guid UserId { get; }
    public string PaymentSystemId { get; }
    public string PaymentSystemUrl { get; }
    public decimal Amount { get; }
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
    public DateTimeOffset? ConfirmationDate { get; private set; }
    public bool IsSuccessful { get; private set; }

    /// <exception cref="TransactionAlreadyAcceptedException"></exception>
    public void AcceptPayment()
    {
        if (IsSuccessful) throw new TransactionAlreadyAcceptedException();
        ConfirmationDate = DateTimeOffset.Now;
        IsSuccessful = true;
        AddDomainEvent(new TransactionAcceptedEvent(UserId, Amount));
    }
}
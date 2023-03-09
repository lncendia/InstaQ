namespace InstaQ.Application.Abstractions.Profile.DTOs;

public class PaymentDto
{
    public PaymentDto(Guid id, string paymentSystemId, decimal amount, DateTimeOffset date, bool isSuccessful, DateTimeOffset? completionDate, string payUrl)
    {
        if (isSuccessful && completionDate == null)
            throw new ArgumentException("Completion date must be set for successful payments");
        Id = id;
        PaymentSystemId = paymentSystemId;
        Amount = amount;
        CreationDate = date;
        IsSuccessful = isSuccessful;
        CompletionDate = completionDate;
        PayUrl = payUrl;
    }

    public Guid Id { get; }
    public string PaymentSystemId { get; }
    public decimal Amount { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? CompletionDate { get; }
    public bool IsSuccessful { get; }
    public string PayUrl { get; }
}
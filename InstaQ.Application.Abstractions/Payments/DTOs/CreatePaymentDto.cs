namespace InstaQ.Application.Abstractions.Payments.DTOs;

public class CreatePaymentDto
{
    public CreatePaymentDto(Guid id, string paymentSystemId, decimal amount, DateTimeOffset creationDate, string payUrl)
    {
        Id = id;
        PaymentSystemId = paymentSystemId;
        Amount = amount;
        CreationDate = creationDate;
        PayUrl = payUrl;
    }

    public Guid Id { get; }
    public string PaymentSystemId { get; }
    public decimal Amount { get; }
    public DateTimeOffset CreationDate { get; }
    public string PayUrl { get; }
}
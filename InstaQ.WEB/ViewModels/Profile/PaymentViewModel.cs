namespace InstaQ.WEB.ViewModels.Profile;

public class PaymentViewModel
{
    public PaymentViewModel(Guid id, decimal amount, DateTimeOffset creationDate, bool isSuccessful, string? payUrl)
    {
        Amount = amount;
        CreationDate = creationDate;
        IsSuccessful = isSuccessful;
        PayUrl = payUrl;
        Id = id;
    }

    public Guid Id { get; }
    public decimal Amount { get; }
    public DateTimeOffset CreationDate { get; }
    public bool IsSuccessful { get; }
    public string? PayUrl { get; }
}
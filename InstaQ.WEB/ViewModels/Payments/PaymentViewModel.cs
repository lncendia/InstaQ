namespace InstaQ.WEB.ViewModels.Payments;

public class PaymentViewModel
{
    public PaymentViewModel(decimal amount, DateTimeOffset creationDate, string payUrl, Guid id)
    {
        Amount = amount.ToString("C");
        CreationDate = creationDate.ToOffset(TimeSpan.FromHours(3)).ToString("d");
        PayUrl = payUrl;
        Id = id;
    }
    public string Amount { get; }
    public string CreationDate { get; }
    public string PayUrl { get; }
    public Guid Id { get; }
}
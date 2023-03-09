namespace InstaQ.Application.Abstractions.Payments.DTOs;

public class PaymentData
{
    public PaymentData(string id, string payUrl)
    {
        BillId = id;
        PayUrl = payUrl;
    }

    public string BillId { get; }
    public string PayUrl { get; }
}
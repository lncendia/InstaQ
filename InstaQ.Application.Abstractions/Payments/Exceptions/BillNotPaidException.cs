namespace InstaQ.Application.Abstractions.Payments.Exceptions;

public class BillNotPaidException : Exception
{
    public BillNotPaidException(string billId) : base($"Bill {billId} has not been paid")
    {
    }
}
namespace InstaQ.Application.Abstractions.Payments.Exceptions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException() : base("Transaction not found")
    {
    }
}
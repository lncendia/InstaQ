namespace InstaQ.Domain.Transactions.Exceptions;

public class SmallAmountException : Exception
{
    public SmallAmountException() : base("Transaction must be more than 100 rubles")
    {
    }
}
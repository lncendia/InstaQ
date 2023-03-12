namespace InstaQ.Domain.Users.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(Guid userId) : base("Insufficient funds in the user's account")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
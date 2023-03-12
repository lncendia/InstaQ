namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class UserBalanceException : Exception
{
    public UserBalanceException(Guid userId) : base("User's balance is less or equal zero")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
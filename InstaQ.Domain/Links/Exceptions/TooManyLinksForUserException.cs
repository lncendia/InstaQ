namespace InstaQ.Domain.Links.Exceptions;

public class TooManyLinksForUserException : Exception
{
    public Guid UserId { get; }

    public TooManyLinksForUserException(Guid userId) : base("User has too many links")
    {
        UserId = userId;
    }
}
namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class UserSubscribeException : Exception
{
    public UserSubscribeException(Guid userId, string name) : base("User is not subscribed")
    {
        UserId = userId;
        Name = name;
    }

    public Guid UserId { get; }
    public string Name { get; }
}
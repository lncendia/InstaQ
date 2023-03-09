namespace InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;

public class LinkedUserNotFoundException:Exception
{
    public Guid UserId { get; }
    public LinkedUserNotFoundException(Guid userId):base("Linked user not found")
    {
        UserId = userId;
    }
}
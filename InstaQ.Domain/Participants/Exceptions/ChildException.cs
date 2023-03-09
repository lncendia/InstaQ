namespace InstaQ.Domain.Participants.Exceptions;

public class ChildException:Exception
{
    public ChildException():base("Child participant can't have children")
    {
    }
}
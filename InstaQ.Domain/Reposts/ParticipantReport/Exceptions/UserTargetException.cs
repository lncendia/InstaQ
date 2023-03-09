namespace InstaQ.Domain.Reposts.ParticipantReport.Exceptions;

public class UserTargetException : Exception
{
    public UserTargetException() : base("Target Pk is not set")
    {
    }
}
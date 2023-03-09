namespace InstaQ.Domain.Participants.Exceptions;

public class TooManyParticipantsException : Exception
{
    public TooManyParticipantsException() : base("The number of chat participants should not exceed 1000")
    {
    }
}
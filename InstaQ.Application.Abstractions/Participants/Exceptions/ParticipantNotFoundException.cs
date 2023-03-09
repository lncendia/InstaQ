namespace InstaQ.Application.Abstractions.Participants.Exceptions;

public class ParticipantNotFoundException : Exception
{
    public ParticipantNotFoundException() : base("Can't find participant")
    {
    }
}
namespace InstaQ.Domain.Participants.Exceptions;

public class ParticipantAlreadyExistsException : Exception
{
    public string Pk { get; }

    public ParticipantAlreadyExistsException(string pk) : base("There is already such a participant")
    {
        Pk = pk;
    }
}
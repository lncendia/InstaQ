namespace InstaQ.Domain.Reposts.ParticipantReport.Exceptions;

public class ParticipantRecordAlreadyExistException : Exception
{
    public ParticipantRecordAlreadyExistException(string pk) : base(
        "Record for this participant already exist")
    {
        Pk = pk;
    }

    public string Pk { get; }
}
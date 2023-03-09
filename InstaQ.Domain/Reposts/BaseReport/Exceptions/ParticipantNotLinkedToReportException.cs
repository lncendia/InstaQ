namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class ParticipantNotLinkedToReportException : Exception
{
    public ParticipantNotLinkedToReportException(Guid id) : base($"Participant is not linked to report")
    {
        ParticipantId = id;
    }

    public Guid ParticipantId { get; }
}
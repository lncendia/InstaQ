using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.BaseReport.Events;

public class ReportFinishedEvent : IDomainEvent
{
    public ReportFinishedEvent(Guid reportId, Guid ownerId, bool success, int countRequests, DateTimeOffset finishedAt)
    {
        ReportId = reportId;
        Success = success;
        FinishedAt = finishedAt;
        OwnerId = ownerId;
        CountRequests = countRequests;
    }

    public Guid ReportId { get; }
    public Guid OwnerId { get; }
    public bool Success { get; }
    public int CountRequests { get; }
    public DateTimeOffset FinishedAt { get; }
}
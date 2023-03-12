using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.BaseReport.Events;

public class ReportDeletedEvent : IDomainEvent
{
    public ReportDeletedEvent(Guid reportId, Guid userId, int countRequests)
    {
        ReportId = reportId;
        OwnerId = userId;
        CountRequests = countRequests;
    }

    public Guid ReportId { get; }
    public Guid OwnerId { get; }
    public int CountRequests { get; }
}
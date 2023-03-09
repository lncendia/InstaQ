using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.BaseReport.Events;

public class ReportDeletedEvent : IDomainEvent
{
    public ReportDeletedEvent(Guid reportId)
    {
        ReportId = reportId;
    }
    
    public Guid ReportId { get; }
}
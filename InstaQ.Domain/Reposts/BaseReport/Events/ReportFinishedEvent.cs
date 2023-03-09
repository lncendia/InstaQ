using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.BaseReport.Events;

public class ReportFinishedEvent : IDomainEvent
{
    public ReportFinishedEvent(Guid reportId, bool success, DateTimeOffset finishedAt)
    {
        ReportId = reportId;
        Success = success;
        FinishedAt = finishedAt;
    }

    public Guid ReportId { get; }
    public bool Success { get; }
    public DateTimeOffset FinishedAt { get; }
}
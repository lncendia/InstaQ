using InstaQ.Domain.Abstractions;
using InstaQ.Domain.ReportLogs.Enums;

namespace InstaQ.Domain.ReportLogs.Entities;

public class ReportLog : AggregateRoot
{
    public ReportLog(Guid userId, Guid reportId, ReportType type, DateTimeOffset createdAt, string additionalInfo)
    {
        UserId = userId;
        ReportId = reportId;
        Type = type;
        CreatedAt = createdAt;
        AdditionalInfo = additionalInfo;
    }


    public Guid UserId { get; }
    public Guid? ReportId { get; private set; }
    public ReportType Type { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? FinishedAt { get; private set; }
    public bool? Success { get; private set; }
    public string AdditionalInfo { get; }
    public bool IsFinished => FinishedAt.HasValue;

    public void Finish(bool success, DateTimeOffset finishedAt)
    {
        Success = success;
        FinishedAt = finishedAt;
    }

    public void ReportDeleted() => ReportId = null;
}
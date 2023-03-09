using InstaQ.Domain.ReportLogs.Enums;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs;

public class ReportShortDto
{
    public ReportShortDto(Guid? id, string? hashtag, ReportType type, DateTimeOffset creationDate, DateTimeOffset? endDate, bool isCompleted, bool isSucceeded)
    {
        Id = id;
        Hashtag = hashtag;
        Type = type;
        CreationDate = creationDate;
        EndDate = endDate;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
    }

    public Guid? Id { get; }
    public string? Hashtag { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
}
using InstaQ.Domain.ReportLogs.Enums;

namespace InstaQ.WEB.ViewModels.Reports;

public class ReportShortViewModel
{
    public ReportShortViewModel(Guid? id, string? hashtag, ReportType type, DateTimeOffset creationDate,
        DateTimeOffset? endDate, bool isCompleted, bool isSucceeded, decimal? amount)
    {
        Id = id;
        Hashtag = hashtag;
        Type = type;
        CreationDate = creationDate;
        EndDate = endDate;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
        Amount = amount;
    }

    public Guid? Id { get; }
    public string? Hashtag { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
    public decimal? Amount { get; }
}
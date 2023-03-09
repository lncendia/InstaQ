namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

public abstract class ReportDto
{
    protected ReportDto(ReportBuilder builder)
    {
        Id = builder.Id ?? throw new ArgumentException("builder not formed", nameof(builder));
        CreationDate = builder.CreationDate ?? throw new ArgumentException("builder not formed", nameof(builder));
        StartDate = builder.StartDate;
        EndDate = builder.EndDate;
        IsStarted = builder.IsStarted;
        IsCompleted = builder.IsCompleted;
        IsSucceeded = builder.IsSucceeded;
        Message = builder.Message;
        ElementsCount = builder.ElementsCount;
    }

    public Guid Id { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? StartDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsStarted { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
    public string? Message { get; }
    public int ElementsCount { get; }
}
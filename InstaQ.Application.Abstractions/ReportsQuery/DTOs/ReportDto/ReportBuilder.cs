namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

public abstract class ReportBuilder
{
    public Guid? Id { get; private set; }
    public DateTimeOffset? CreationDate { get; private set; }
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsSucceeded { get; private set; }
    public string? Message { get; private set; }
    public int ElementsCount { get; private set; }

    public ReportBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public ReportBuilder WithDates(DateTimeOffset startDate, DateTimeOffset? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
        return this;
    }

    public ReportBuilder WithCreationDate(DateTimeOffset creationDate)
    {
        CreationDate = creationDate;
        return this;
    }

    public ReportBuilder WithStatus(bool isCompleted, bool isSucceeded)
    {
        IsStarted = true;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
        return this;
    }

    public ReportBuilder WithMessage(string? message)
    {
        Message = message;
        return this;
    }

    public ReportBuilder WithElements(int count)
    {
        ElementsCount = count;
        return this;
    }
}
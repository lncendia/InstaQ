using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Reposts.BaseReport.Events;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Users.Entities;

namespace InstaQ.Domain.Reposts.BaseReport.Entities;

public abstract class Report : AggregateRoot
{
    private protected Report(User user)
    {
        if (user.Balance <= 0) throw new UserBalanceException(user.Id);
        UserId = user.Id;
    }


    public Guid UserId { get; }
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public int RequestsCount { get; private set; }
    public bool IsSucceeded { get; private set; }
    public string? Message { get; private set; }

    public bool IsStarted => StartDate.HasValue;
    public bool IsCompleted => EndDate.HasValue;

    public void AddRequests(int count)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (!IsStarted) throw new ReportNotStartedException(Id);
        RequestsCount += count;
    }

    protected readonly List<ReportElement> ReportElementsList = new();

    protected void Start() => StartDate = DateTimeOffset.Now;

    protected void Succeed()
    {
        EndDate = DateTimeOffset.Now;
        IsSucceeded = true;
        AddDomainEvent(new ReportFinishedEvent(Id, UserId, true, RequestsCount, EndDate.Value));
    }

    protected void Fail(string message)
    {
        if (!IsStarted) StartDate = DateTimeOffset.Now;
        EndDate = DateTimeOffset.Now;
        IsSucceeded = false;
        Message = message;
        AddDomainEvent(new ReportFinishedEvent(Id, UserId, false, RequestsCount, EndDate.Value));
    }
}
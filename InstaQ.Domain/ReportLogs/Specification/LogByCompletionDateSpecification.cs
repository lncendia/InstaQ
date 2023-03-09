using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.ReportLogs.Specification;

public class LogByCompletionDateSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByCompletionDateSpecification(DateTimeOffset maxDate, DateTimeOffset minDate)
    {
        MaxDate = maxDate;
        MinDate = minDate;
    }

    public DateTimeOffset MaxDate { get; }
    public DateTimeOffset MinDate { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.FinishedAt >= MinDate && item.CreatedAt <= MaxDate;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}
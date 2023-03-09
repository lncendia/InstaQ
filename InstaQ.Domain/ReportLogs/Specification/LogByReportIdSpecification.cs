using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.ReportLogs.Specification;

public class LogByReportIdSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByReportIdSpecification(Guid id) => Id = id;

    public Guid Id { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.UserId == Id;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}
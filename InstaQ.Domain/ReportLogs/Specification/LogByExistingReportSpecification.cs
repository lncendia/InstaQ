using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.ReportLogs.Specification;

public class LogByExistingReportSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByExistingReportSpecification(bool isExist) => IsExist = isExist;
    public bool IsExist { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.ReportId.HasValue == IsExist;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}
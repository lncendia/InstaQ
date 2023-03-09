using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Enums;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.ReportLogs.Specification;

public class LogByReportTypeSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{

    public ReportType Type { get; }

    public LogByReportTypeSpecification(ReportType type) => Type = type;

    public bool IsSatisfiedBy(ReportLog item) => item.Type == Type;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}
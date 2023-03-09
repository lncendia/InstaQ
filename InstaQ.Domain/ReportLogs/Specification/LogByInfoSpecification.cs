using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.ReportLogs.Specification;

public class LogByInfoSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByInfoSpecification(string hashtag) => Hashtag = hashtag;

    public string Hashtag { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.AdditionalInfo == Hashtag;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}
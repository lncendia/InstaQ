using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.ReportLogs.Specification.Visitor;

public interface IReportLogSpecificationVisitor : ISpecificationVisitor<IReportLogSpecificationVisitor, ReportLog>
{
    void Visit(LogByCreationDateSpecification specification);
    void Visit(LogByCompletionDateSpecification specification);
    void Visit(LogByUserIdSpecification specification);
    void Visit(LogByReportIdSpecification specification);
    void Visit(LogByInfoSpecification specification);
    void Visit(LogByReportTypeSpecification specification);
    void Visit(LogByExistingReportSpecification specification);
}
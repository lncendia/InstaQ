using System.Linq.Expressions;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Specification;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class LogVisitor : BaseVisitor<LogModel, IReportLogSpecificationVisitor, ReportLog>,
    IReportLogSpecificationVisitor
{
    protected override Expression<Func<LogModel, bool>> ConvertSpecToExpression(
        ISpecification<ReportLog, IReportLogSpecificationVisitor> spec)
    {
        var visitor = new LogVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(LogByCreationDateSpecification specification) => Expr = x =>
        x.CreatedAt >= specification.MinDate && x.CreatedAt <= specification.MaxDate;

    public void Visit(LogByCompletionDateSpecification specification) =>
        Expr = x =>
            x.FinishedAt >= specification.MinDate && x.FinishedAt <= specification.MaxDate;

    public void Visit(LogByUserIdSpecification specification) => Expr = x => x.UserId == specification.Id;

    public void Visit(LogByReportIdSpecification specification) => Expr = x => x.ReportId == specification.Id;
    public void Visit(LogByInfoSpecification specification) => Expr = x => x.AdditionalInfo == specification.Hashtag;

    public void Visit(LogByReportTypeSpecification specification) => Expr = x => x.Type == specification.Type;

    public void Visit(LogByExistingReportSpecification specification) =>
        Expr = x => x.ReportId.HasValue == specification.IsExist;
}
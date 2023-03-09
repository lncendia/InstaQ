using System.Linq.Expressions;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.CommentReport.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models.Reports.CommentReport;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class CommentReportVisitor : BaseVisitor<CommentReportModel, ICommentReportSpecificationVisitor, CommentReport>,
    ICommentReportSpecificationVisitor
{
    protected override Expression<Func<CommentReportModel, bool>> ConvertSpecToExpression(
        ISpecification<CommentReport, ICommentReportSpecificationVisitor> spec)
    {
        var visitor = new CommentReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}
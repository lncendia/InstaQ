using System.Linq.Expressions;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class LikeReportVisitor : BaseVisitor<LikeReportModel, ILikeReportSpecificationVisitor, LikeReport>,
    ILikeReportSpecificationVisitor
{
    protected override Expression<Func<LikeReportModel, bool>> ConvertSpecToExpression(
        ISpecification<LikeReport, ILikeReportSpecificationVisitor> spec)
    {
        var visitor = new LikeReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}
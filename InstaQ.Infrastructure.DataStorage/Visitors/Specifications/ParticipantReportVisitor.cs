using System.Linq.Expressions;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class ParticipantReportVisitor : BaseVisitor<ParticipantReportModel, IParticipantReportSpecificationVisitor, ParticipantReport>, IParticipantReportSpecificationVisitor
{
    protected override Expression<Func<ParticipantReportModel, bool>> ConvertSpecToExpression(
        ISpecification<ParticipantReport, IParticipantReportSpecificationVisitor> spec)
    {
        var visitor = new ParticipantReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}
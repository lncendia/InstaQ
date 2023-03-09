using InstaQ.Domain.Ordering.Abstractions;

namespace InstaQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;

public interface IParticipantReportSortingVisitor : ISortingVisitor<IParticipantReportSortingVisitor, Entities.ParticipantReport>
{
}
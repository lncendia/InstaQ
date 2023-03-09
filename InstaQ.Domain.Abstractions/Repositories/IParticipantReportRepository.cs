using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;
using InstaQ.Domain.Reposts.ParticipantReport.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface IParticipantReportRepository : IRepository<ParticipantReport, IParticipantReportSpecificationVisitor, IParticipantReportSortingVisitor>
{
}
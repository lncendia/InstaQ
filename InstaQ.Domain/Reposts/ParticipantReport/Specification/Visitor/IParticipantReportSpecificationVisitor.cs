using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Reposts.ParticipantReport.Specification.Visitor;

public interface
    IParticipantReportSpecificationVisitor : ISpecificationVisitor<IParticipantReportSpecificationVisitor,
        Entities.ParticipantReport>
{
}
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Participants.Specification.Visitor;

public interface IParticipantSpecificationVisitor : ISpecificationVisitor<IParticipantSpecificationVisitor, Participant>
{
    void Visit(ParticipantsByUserIdSpecification specification);
    void Visit(ParticipantsByNameSpecification specification);
    void Visit(VipParticipantsSpecification specification);
    void Visit(ParentParticipantsSpecification specification);
    void Visit(ChildParticipantsSpecification specification);
}
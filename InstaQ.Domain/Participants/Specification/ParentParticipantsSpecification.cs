using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Participants.Specification;

public class ParentParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public bool IsSatisfiedBy(Participant item) => item.ParentParticipantId == null;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}
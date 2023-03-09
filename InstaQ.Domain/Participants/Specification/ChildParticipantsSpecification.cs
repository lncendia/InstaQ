using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Participants.Specification;

public class ChildParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ChildParticipantsSpecification(Guid parentId) => ParentId = parentId;

    public Guid ParentId { get; }
    public bool IsSatisfiedBy(Participant item) => item.ParentParticipantId == ParentId;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}
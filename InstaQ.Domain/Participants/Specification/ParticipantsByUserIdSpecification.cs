using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Participants.Specification;

public class ParticipantsByUserIdSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByUserIdSpecification(Guid userId) => UserId = userId;

    public Guid UserId { get; }

    public bool IsSatisfiedBy(Participant item) => item.UserId == UserId;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Participants.Specification;

public class ParticipantsByNameSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByNameSpecification(string name) => Name = name;

    public string Name { get; }

    public bool IsSatisfiedBy(Participant item) => item.Name.Contains(Name);

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}
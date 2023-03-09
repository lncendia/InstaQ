using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Links.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Links.Specification;

public class LinkByUserIdSpecification : ISpecification<Link, ILinkSpecificationVisitor>
{
    public Guid Id { get; }
    public LinkByUserIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(Link item) => item.User1Id == Id || item.User2Id == Id;

    public void Accept(ILinkSpecificationVisitor visitor) => visitor.Visit(this);
}
using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Links.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Links.Specification;

public class AcceptedLinkSpecification : ISpecification<Link, ILinkSpecificationVisitor>
{
    public bool IsSatisfiedBy(Link item) => item.IsConfirmed;

    public void Accept(ILinkSpecificationVisitor visitor) => visitor.Visit(this);
}
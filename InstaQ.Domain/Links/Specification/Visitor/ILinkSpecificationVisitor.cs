using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Links.Specification.Visitor;

public interface ILinkSpecificationVisitor : ISpecificationVisitor<ILinkSpecificationVisitor, Link>
{
    void Visit(LinkByUserIdSpecification specification);
    void Visit(LinkByUserIdsSpecification specification);
    void Visit(AcceptedLinkSpecification specification);
}
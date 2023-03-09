using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Links.Ordering.Visitor;
using InstaQ.Domain.Links.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface ILinkRepository:IRepository<Link, ILinkSpecificationVisitor, ILinkSortingVisitor>
{
    
}
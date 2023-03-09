using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Ordering.Abstractions;

namespace InstaQ.Domain.Links.Ordering.Visitor;

public interface ILinkSortingVisitor : ISortingVisitor<ILinkSortingVisitor, Link>
{
}
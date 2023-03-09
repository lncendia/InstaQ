using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Links.Ordering.Visitor;
using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class LinkSortingVisitor : BaseSortingVisitor<LinkModel, ILinkSortingVisitor, Link>, ILinkSortingVisitor
{
    protected override List<SortData<LinkModel>> ConvertOrderToList(IOrderBy<Link, ILinkSortingVisitor> spec)
    {
        var visitor = new LinkSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}
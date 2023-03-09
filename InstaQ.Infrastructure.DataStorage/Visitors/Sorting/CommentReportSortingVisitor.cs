using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.CommentReport.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models.Reports.CommentReport;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class CommentReportSortingVisitor : BaseSortingVisitor<CommentReportModel, ICommentReportSortingVisitor, CommentReport>, ICommentReportSortingVisitor
{
    protected override List<SortData<CommentReportModel>> ConvertOrderToList(IOrderBy<CommentReport, ICommentReportSortingVisitor> spec)
    {
        var visitor = new CommentReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}
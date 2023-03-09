using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models.Reports.LikeReport;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class LikeReportSortingVisitor : BaseSortingVisitor<LikeReportModel, ILikeReportSortingVisitor, LikeReport>, ILikeReportSortingVisitor
{
    protected override List<SortData<LikeReportModel>> ConvertOrderToList(IOrderBy<LikeReport, ILikeReportSortingVisitor> spec)
    {
        var visitor = new LikeReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}
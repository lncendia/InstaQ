using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Ordering;
using InstaQ.Domain.ReportLogs.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class LogSortingVisitor : BaseSortingVisitor<LogModel, IReportLogSortingVisitor, ReportLog>,
    IReportLogSortingVisitor
{
    protected override List<SortData<LogModel>> ConvertOrderToList(IOrderBy<ReportLog, IReportLogSortingVisitor> spec)
    {
        var visitor = new LogSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(LogByDateOrder order) => SortItems.Add(new SortData<LogModel>(x => x.CreatedAt, false));
}
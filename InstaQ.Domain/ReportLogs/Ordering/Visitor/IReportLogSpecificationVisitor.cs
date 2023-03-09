using InstaQ.Domain.Ordering.Abstractions;

namespace InstaQ.Domain.ReportLogs.Ordering.Visitor;

public interface IReportLogSortingVisitor : ISortingVisitor<IReportLogSortingVisitor, Entities.ReportLog>
{
    void Visit(LogByDateOrder order);
}
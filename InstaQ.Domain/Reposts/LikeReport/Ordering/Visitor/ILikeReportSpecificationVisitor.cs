using InstaQ.Domain.Ordering.Abstractions;

namespace InstaQ.Domain.Reposts.LikeReport.Ordering.Visitor;

public interface ILikeReportSortingVisitor : ISortingVisitor<ILikeReportSortingVisitor, Entities.LikeReport>
{
}
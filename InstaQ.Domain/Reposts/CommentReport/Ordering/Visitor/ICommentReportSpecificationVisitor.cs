using InstaQ.Domain.Ordering.Abstractions;

namespace InstaQ.Domain.Reposts.CommentReport.Ordering.Visitor;

public interface ICommentReportSortingVisitor : ISortingVisitor<ICommentReportSortingVisitor, Entities.CommentReport>
{
}
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.CommentReport.Ordering.Visitor;
using InstaQ.Domain.Reposts.CommentReport.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface ICommentReportRepository : IRepository<CommentReport, ICommentReportSpecificationVisitor, ICommentReportSortingVisitor>
{
}
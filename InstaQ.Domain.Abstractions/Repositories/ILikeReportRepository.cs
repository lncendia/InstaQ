using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Ordering.Visitor;
using InstaQ.Domain.Reposts.LikeReport.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface ILikeReportRepository : IRepository<LikeReport, ILikeReportSpecificationVisitor, ILikeReportSortingVisitor>
{
}
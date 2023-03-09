using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Ordering.Visitor;
using InstaQ.Domain.ReportLogs.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface IReportLogRepository : IRepository<ReportLog, IReportLogSpecificationVisitor, IReportLogSortingVisitor>
{
}
using InstaQ.Domain.Reposts.BaseReport.Entities;

namespace InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportInitializerUnit<in T> where T : Report
{
    Task InitializeReportAsync(T report, CancellationToken token);
}
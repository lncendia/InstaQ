using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Domain.Reposts.BaseReport.Entities;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;

namespace InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportProcessorUnit<in T> where T : Report
{
    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ReportNotStartedException">Report not initialized</exception>/// <exception cref="TooManyRequestErrorsException">Too many request errors</exception>
    Task ProcessReportAsync(T report, CancellationToken token);
}
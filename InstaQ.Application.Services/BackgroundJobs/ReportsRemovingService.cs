using InstaQ.Application.Abstractions.BackgroundJobs.ServicesInterfaces;
using InstaQ.Application.Abstractions.Jobs.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Enums;
using InstaQ.Domain.ReportLogs.Specification;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications;

namespace InstaQ.Application.Services.BackgroundJobs;

public class ReportsRemovingService : IReportsRemovingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJobStorage _jobStorage;
    private static readonly DateTimeOffset MinDate = DateTimeOffset.MinValue;
    private static readonly DateTimeOffset MaxDate = DateTimeOffset.Now.AddDays(-15);

    public ReportsRemovingService(IUnitOfWork unitOfWork, IJobStorage jobStorage)
    {
        _unitOfWork = unitOfWork;
        _jobStorage = jobStorage;
    }

    public async Task CheckAndRemoveAsync()
    {
        var spec = new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(
            new LogByExistingReportSpecification(true), new LogByCompletionDateSpecification(MaxDate, MinDate));
        var reports =
            await _unitOfWork.ReportLogRepository.Value.FindAsync(spec);
        foreach (var report in reports)
        {
            var task = report.Type switch
            {
                ReportType.Likes => ProcessLikeReportAsync(report.ReportId!.Value),
                ReportType.Participants => ProcessParticipantReportAsync(report.ReportId!.Value),
                ReportType.Comments => ProcessCommentReportAsync(report.ReportId!.Value),
                _ => throw new ArgumentOutOfRangeException()
            };
            await Task.WhenAll(task, _jobStorage.DeleteJobIdAsync(report.ReportId.Value));
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private Task ProcessLikeReportAsync(Guid id) => _unitOfWork.LikeReportRepository.Value.DeleteAsync(id);
    private Task ProcessCommentReportAsync(Guid id) => _unitOfWork.CommentReportRepository.Value.DeleteAsync(id);

    private Task ProcessParticipantReportAsync(Guid id) =>
        _unitOfWork.ParticipantReportRepository.Value.DeleteAsync(id);
}
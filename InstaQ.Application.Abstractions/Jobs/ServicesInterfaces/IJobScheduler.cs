namespace InstaQ.Application.Abstractions.Jobs.ServicesInterfaces;

public interface IJobScheduler
{
    public Task<string> StartLikeReportAsync(Guid reportId, DateTimeOffset? startAt = null);
    public Task<string> StartCommentReportAsync(Guid reportId, DateTimeOffset? startAt = null);
    public Task<string> StartParticipantReportAsync(Guid reportId, DateTimeOffset? startAt = null);
    public Task<bool> CancelAsync(string? jobId);
}
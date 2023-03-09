using Hangfire;
using InstaQ.Application.Abstractions.Jobs.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;

namespace InstaQ.Infrastructure.BackgroundTasks;

public class JobScheduler : IJobScheduler
{
    private readonly IBackgroundJobClient _client;
    public JobScheduler(IBackgroundJobClient client) => _client = client;

    public Task<string> StartLikeReportAsync(Guid reportId, DateTimeOffset? startAt = null)
    {
        var id = startAt.HasValue
            ? _client.Schedule<IReportStarter>(
                starter => starter.StartLikeReportAsync(reportId, CancellationToken.None), startAt.Value)
            : _client.Enqueue<IReportStarter>(x =>
                x.StartLikeReportAsync(reportId, CancellationToken.None));

        return Task.FromResult(id);
    }

    public Task<string> StartCommentReportAsync(Guid reportId, DateTimeOffset? startAt = null)
    {
        var id = startAt.HasValue
            ? _client.Schedule<IReportStarter>(
                starter => starter.StartCommentReportAsync(reportId, CancellationToken.None), startAt.Value)
            : _client.Enqueue<IReportStarter>(x =>
                x.StartCommentReportAsync(reportId, CancellationToken.None));

        return Task.FromResult(id);
    }

    public Task<string> StartParticipantReportAsync(Guid reportId, DateTimeOffset? startAt = null)
    {
        var id = startAt.HasValue
            ? _client.Schedule<IReportStarter>(
                starter => starter.StartParticipantReportAsync(reportId, CancellationToken.None), startAt.Value)
            : _client.Enqueue<IReportStarter>(x =>
                x.StartParticipantReportAsync(reportId, CancellationToken.None));
        return Task.FromResult(id);
    }

    public Task<bool> CancelAsync(string? jobId) => Task.FromResult(_client.Delete(jobId));
}
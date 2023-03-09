namespace InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;

public interface IReportStarter
{
    Task StartLikeReportAsync(Guid id, CancellationToken token);
    Task StartCommentReportAsync(Guid id, CancellationToken token);
    Task StartParticipantReportAsync(Guid id, CancellationToken token);
}
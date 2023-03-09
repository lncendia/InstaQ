using InstaQ.Application.Abstractions.ReportsManagement.DTOs;

namespace InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;

public interface IReportCreationService
{
    Task CreateLikeReportAsync(PublicationReportCreateDto dto, TimeSpan? startAt = null);
    Task CreateCommentReportAsync(PublicationReportCreateDto dto, TimeSpan? startAt = null);
    Task CreateParticipantReportAsync(Guid userId, TimeSpan? startAt = null);
    Task CancelLikeReportAsync(Guid reportId, Guid userId);
    Task CancelCommentReportAsync(Guid reportId, Guid userId);
    Task CancelParticipantReportAsync(Guid reportId, Guid userId);
}
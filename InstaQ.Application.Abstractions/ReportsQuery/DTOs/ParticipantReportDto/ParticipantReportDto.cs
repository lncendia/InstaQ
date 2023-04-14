namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportDto : ReportDto.ReportDto
{
    public ParticipantReportDto(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        int requestsCount) : base(id, creationDate, startDate, endDate, isStarted, isCompleted, isSucceeded, message,
        elementsCount, requestsCount)
    {
    }
}
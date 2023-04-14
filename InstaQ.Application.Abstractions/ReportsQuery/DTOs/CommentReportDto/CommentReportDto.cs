namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;

public class CommentReportDto : PublicationReportDto.PublicationReportDto
{
    public CommentReportDto(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        int requestsCount, string hashtag, int publicationsCount, int process, bool allParticipants,
        IEnumerable<string> linkedUsers) : base(id, creationDate, startDate, endDate, isStarted, isCompleted,
        isSucceeded, message, elementsCount, requestsCount, hashtag, publicationsCount, process, allParticipants,
        linkedUsers)
    {
    }
}
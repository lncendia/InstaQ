namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

public abstract class PublicationReportDto : ReportDto.ReportDto
{
    protected PublicationReportDto(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        int requestsCount, string hashtag, int publicationsCount, int process, bool allParticipants,
        IEnumerable<string> linkedUsers) : base(id, creationDate, startDate, endDate, isStarted, isCompleted,
        isSucceeded, message, elementsCount, requestsCount)
    {
        Hashtag = hashtag;
        PublicationsCount = publicationsCount;
        Process = process;
        AllParticipants = allParticipants;
        LinkedUsers.AddRange(linkedUsers);
    }

    public List<string> LinkedUsers { get; } = new();
    public string Hashtag { get; }
    public int PublicationsCount { get; }
    public int Process { get; }
    public bool AllParticipants { get; }
}
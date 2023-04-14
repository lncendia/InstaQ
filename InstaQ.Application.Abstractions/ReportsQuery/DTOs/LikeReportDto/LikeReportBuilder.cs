using InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;

public class LikeReportBuilder : PublicationReportBuilder
{
    private LikeReportBuilder()
    {
    }

    public static LikeReportBuilder LikeReportDto() => new();

    public LikeReportDto Build()
    {
        if (Id == null) throw new InvalidOperationException("builder not formed");
        if (CreationDate == null) throw new InvalidOperationException("builder not formed");
        if (string.IsNullOrEmpty(Hashtag)) throw new InvalidOperationException("builder not formed");
        LinkedUsers ??= Enumerable.Empty<string>();
        return new LikeReportDto(Id.Value, CreationDate.Value, StartDate, EndDate, IsStarted, IsCompleted, IsSucceeded, Message,
            ElementsCount, RequestsCount, Hashtag, PublicationsCount, Process, AllParticipants, LinkedUsers);
    }
}
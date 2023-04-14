using InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;

public class CommentReportBuilder : PublicationReportBuilder
{
    private CommentReportBuilder()
    {
    }

    public static CommentReportBuilder CommentReportDto() => new();

    public CommentReportDto Build()
    {
        if (Id == null) throw new InvalidOperationException("builder not formed");
        if (CreationDate == null) throw new InvalidOperationException("builder not formed");
        if (string.IsNullOrEmpty(Hashtag)) throw new InvalidOperationException("builder not formed");
        LinkedUsers ??= Enumerable.Empty<string>();
        return new CommentReportDto(Id.Value, CreationDate.Value, StartDate, EndDate, IsStarted, IsCompleted, IsSucceeded, Message,
            ElementsCount, RequestsCount, Hashtag, PublicationsCount, Process, AllParticipants, LinkedUsers);
    }
}
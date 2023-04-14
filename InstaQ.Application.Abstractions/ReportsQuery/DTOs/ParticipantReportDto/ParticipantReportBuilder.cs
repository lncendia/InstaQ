using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportBuilder : ReportBuilder
{

    private ParticipantReportBuilder()
    {
    }

    public static ParticipantReportBuilder ParticipantReportDto() => new();

    public ParticipantReportDto Build()
    {
        if (Id == null) throw new InvalidOperationException("builder not formed");
        if (CreationDate == null) throw new InvalidOperationException("builder not formed");
        return new ParticipantReportDto(Id.Value, CreationDate.Value, StartDate, EndDate, IsStarted, IsCompleted, IsSucceeded, Message,
            ElementsCount, RequestsCount);
    }
}
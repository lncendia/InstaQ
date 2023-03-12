using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportBuilder : ReportBuilder
{

    private ParticipantReportBuilder()
    {
    }

    public static ParticipantReportBuilder ParticipantReportDto() => new();

    public ParticipantReportDto Build() => new(this);
}
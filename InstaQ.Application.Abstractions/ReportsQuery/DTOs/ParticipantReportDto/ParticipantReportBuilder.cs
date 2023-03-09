using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportBuilder : ReportBuilder
{
    public string? Pk;

    private ParticipantReportBuilder()
    {
    }

    public static ParticipantReportBuilder ParticipantReportDto() => new();

    public ParticipantReportBuilder WithVk(string pk)
    {
        Pk = pk;
        return this;
    }

    public ParticipantReportDto Build() => new(this);
}
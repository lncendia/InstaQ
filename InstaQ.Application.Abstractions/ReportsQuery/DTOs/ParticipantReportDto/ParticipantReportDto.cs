namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportDto : ReportDto.ReportDto
{
    public ParticipantReportDto(ParticipantReportBuilder builder) : base(builder)
    {
        Pk = builder.Pk ?? throw new ArgumentException("builder not formed", nameof(builder));
    }

    public string Pk { get; }
}
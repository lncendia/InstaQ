namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

public abstract class PublicationReportDto : ReportDto.ReportDto
{
    protected PublicationReportDto(PublicationReportBuilder builder) : base(builder)
    {
        Hashtag = builder.Hashtag ?? throw new ArgumentException("builder not formed", nameof(builder));
        SearchStartDate = builder.SearchStartDate;
        if (builder.LinkedUsers != null) LinkedUsers.AddRange(builder.LinkedUsers);
        PublicationsCount = builder.PublicationsCount;
        Process = builder.Process;
        AllParticipants = builder.AllParticipants;
    }

    public List<string> LinkedUsers { get; } = new();
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }
    public int PublicationsCount { get; }
    public int Process { get; }
    public bool AllParticipants { get; }
}
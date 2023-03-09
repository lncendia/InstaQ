namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class ReportNotCompletedException : Exception
{
    public ReportNotCompletedException(Guid reportId) : base("Report not completed")
    {
        ReportId = reportId;
    }
    public Guid ReportId { get; }
}
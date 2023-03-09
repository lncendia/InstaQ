namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class ReportAlreadyStartedException : Exception
{
    public ReportAlreadyStartedException(Guid reportId) : base("Report already started")
    {
        ReportId = reportId;
    }

    public Guid ReportId { get; }
}
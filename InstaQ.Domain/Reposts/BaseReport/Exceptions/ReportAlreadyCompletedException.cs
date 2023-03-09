namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class ReportAlreadyCompletedException : Exception
{
    public ReportAlreadyCompletedException(Guid reportId) : base("Report already completed")
    {
        ReportId = reportId;
    }
    
    public Guid ReportId { get; }
}
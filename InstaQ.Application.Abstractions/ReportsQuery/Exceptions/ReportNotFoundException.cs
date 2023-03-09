namespace InstaQ.Application.Abstractions.ReportsQuery.Exceptions;

public class ReportNotFoundException : Exception
{
    public ReportNotFoundException() : base("Report not found")
    {
    }
}
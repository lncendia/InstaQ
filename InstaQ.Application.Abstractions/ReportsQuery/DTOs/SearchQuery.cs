using InstaQ.Domain.ReportLogs.Enums;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs;

public class SearchQuery
{
    public SearchQuery(int page, ReportType? reportType, DateTimeOffset? from, DateTimeOffset? to)
    {
        Page = page;
        ReportType = reportType;
        From = from;
        To = to;
    }

    public int Page { get; }
    public ReportType? ReportType { get; }
    public DateTimeOffset? From { get; }
    public DateTimeOffset? To { get; }
}
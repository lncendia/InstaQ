using System.ComponentModel.DataAnnotations;
using InstaQ.Domain.ReportLogs.Enums;

namespace InstaQ.WEB.ViewModels.Reports;

public class ReportsSearchQueryViewModel
{
    public int Page { get; set; } = 1;
    public ReportType? ReportType { get; set; }
    [DataType(DataType.DateTime)] public DateTimeOffset? From { get; set; }
    [DataType(DataType.DateTime)] public DateTimeOffset? To { get; set; }
}
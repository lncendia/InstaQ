using System.ComponentModel.DataAnnotations;
using InstaQ.Domain.ReportLogs.Enums;

namespace InstaQ.WEB.ViewModels.ReportsManager;

public class CancelReportViewModel
{
    [Required] public Guid Id { get; set; }
    [Required] public ReportType ReportType { get; set; }
}
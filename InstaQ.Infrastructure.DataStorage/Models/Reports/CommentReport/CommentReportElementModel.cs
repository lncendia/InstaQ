using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.CommentReport;

public class CommentReportElementModel : PublicationReportElementModel
{
    public string Comments { get; set; } = null!;
    public Guid Id { get; set; }
    public CommentReportModel Report { get; set; } = null!;
}
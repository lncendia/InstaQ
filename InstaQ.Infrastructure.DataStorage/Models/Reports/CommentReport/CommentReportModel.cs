using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.CommentReport;

public class CommentReportModel : PublicationReportModel
{
    public List<CommentReportElementModel> ElementsList { get; set; } = new();
}
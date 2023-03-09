using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeReportModel : PublicationReportModel
{
    public List<LikeReportElementModel> ElementsList { get; set; } = new();
}
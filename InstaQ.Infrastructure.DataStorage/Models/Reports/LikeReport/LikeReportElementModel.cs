using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeReportElementModel : PublicationReportElementModel
{
    public string Likes { get; set; } = null!;
    public Guid Id { get; set; }
    public LikeReportModel Report { get; set; } = null!;
}
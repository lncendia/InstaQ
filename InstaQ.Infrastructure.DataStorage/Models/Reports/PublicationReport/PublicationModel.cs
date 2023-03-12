using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public class PublicationModel : IEntityModel
{
    public int Id { get; set; }
    public int EntityId { get; set; }
    public string OwnerPk { get; set; } = null!;
    public string Pk { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool? IsLoaded { get; set; }
    public PublicationReportModel Report { get; set; } = null!;
}
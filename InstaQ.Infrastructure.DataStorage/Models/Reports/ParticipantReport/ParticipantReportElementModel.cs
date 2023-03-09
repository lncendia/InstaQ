using InstaQ.Domain.Reposts.ParticipantReport.Enums;
using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportElementModel : ElementModel
{
    public string? NewName { get; set; }
    public Guid? ParticipantId { get; set; }
    public ElementType? Type { get; set; }
    public int? OwnerId { get; set; }
    
    public ParticipantReportModel Report { get; set; } = null!;
}
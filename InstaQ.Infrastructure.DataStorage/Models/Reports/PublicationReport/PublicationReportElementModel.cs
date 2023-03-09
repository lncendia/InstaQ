using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public abstract class PublicationReportElementModel : ElementModel
{
    public string LikeChatName { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public bool IsAccepted { get; set; }
    public bool Vip { get; set; }
    public string? Note { get; set; }
    public int? OwnerId { get; set; }
}
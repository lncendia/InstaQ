using InstaQ.Domain.Users.Enums;
using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportModel : ReportModel
{
    public string Pk { get; set; } = null!;
    public ParticipantsType Type { get; set; }
    public List<ParticipantReportElementModel> ElementsList { get; set; } = new();
}
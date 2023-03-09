using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Reports;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

namespace InstaQ.WEB.Mappers.ReportMapper;

public class ParticipantReportMapper : IReportMapperUnit<ParticipantReportDto, ParticipantReportViewModel>
{
    public ParticipantReportViewModel Map(ParticipantReportDto report)
    {
        return new ParticipantReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.Pk);
    }
}
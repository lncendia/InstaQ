using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Reports;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;

namespace InstaQ.WEB.Mappers.ReportMapper;

public class LikeReportMapper : IReportMapperUnit<LikeReportDto, LikeReportViewModel>
{
    public LikeReportViewModel Map(LikeReportDto report)
    {
        return new LikeReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.Hashtag, report.SearchStartDate, report.Process, report.PublicationsCount);
    }
}
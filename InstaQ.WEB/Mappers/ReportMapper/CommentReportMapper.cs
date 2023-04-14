using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.ViewModels.Reports;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;

namespace InstaQ.WEB.Mappers.ReportMapper;

public class CommentReportMapper : IReportMapperUnit<CommentReportDto, CommentReportViewModel>
{
    public CommentReportViewModel Map(CommentReportDto report)
    {
        return new CommentReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.Hashtag, report.Process, report.PublicationsCount, report.AllParticipants, report.RequestsCount);
    }
}
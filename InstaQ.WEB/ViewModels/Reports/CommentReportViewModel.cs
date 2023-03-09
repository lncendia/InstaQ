using InstaQ.WEB.ViewModels.Reports.Base;

namespace InstaQ.WEB.ViewModels.Reports;

public class CommentReportViewModel : PublicationReportViewModel
{
    public CommentReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        string hashtag, DateTimeOffset? searchStartDate, int process, int publicationsCount) : base(id, creationDate,
        startDate, endDate, isStarted, isCompleted, isSucceeded, message, elementsCount, hashtag, searchStartDate,
        process, publicationsCount)
    {
    }
}
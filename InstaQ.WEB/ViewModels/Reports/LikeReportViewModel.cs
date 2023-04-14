using InstaQ.WEB.ViewModels.Reports.Base;

namespace InstaQ.WEB.ViewModels.Reports;

public class LikeReportViewModel : PublicationReportViewModel
{
    public LikeReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate, DateTimeOffset? endDate,
        bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount, string hashtag,
        int process, int publicationsCount, bool allParticipants, int requestsCount) : base(id, creationDate, startDate,
        endDate, isStarted, isCompleted, isSucceeded, message, elementsCount, hashtag, process, publicationsCount,
        allParticipants, requestsCount)
    {
    }
}
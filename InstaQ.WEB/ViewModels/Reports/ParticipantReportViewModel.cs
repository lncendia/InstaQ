using InstaQ.WEB.ViewModels.Reports.Base;

namespace InstaQ.WEB.ViewModels.Reports;

public class ParticipantReportViewModel : ReportViewModel
{
    public ParticipantReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount)
        : base(id, creationDate, startDate, endDate, isStarted, isCompleted, isSucceeded, message,
            elementsCount)
    {
    }
}
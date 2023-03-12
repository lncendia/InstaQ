namespace InstaQ.WEB.ViewModels.Reports.Base;

public abstract class PublicationReportViewModel : ReportViewModel
{
    protected PublicationReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        string hashtag, int process, int publicationsCount) : base(id, creationDate, startDate, endDate, isStarted,
        isCompleted, isSucceeded, message, elementsCount)
    {
        Hashtag = hashtag;
        Process = process;
        PublicationsCount = publicationsCount;
    }

    public string Hashtag { get; }
    public int Process { get; }
    public int PublicationsCount { get; }
}
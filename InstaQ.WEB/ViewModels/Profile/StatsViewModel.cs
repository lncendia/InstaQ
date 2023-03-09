namespace InstaQ.WEB.ViewModels.Profile;

public class StatsViewModel
{
    public StatsViewModel(int participantsCount, int reportsCount, int lastMonthReportsCount)
    {
        ParticipantsCount = participantsCount;
        ReportsCount = reportsCount;
        LastMonthReportsCount = lastMonthReportsCount;
    }

    public int ParticipantsCount { get; }
    public int ReportsCount { get; }
    public int LastMonthReportsCount { get; }
}
namespace InstaQ.Application.Abstractions.Profile.DTOs;

public class StatsDto
{
    public StatsDto(int participantsCount, int reportsCount, int reportsThisMonthCount)
    {
        ParticipantsCount = participantsCount;
        ReportsCount = reportsCount;
        ReportsThisMonthCount = reportsThisMonthCount;
    }

    public int ParticipantsCount { get; }
    public int ReportsCount { get; }
    public int ReportsThisMonthCount { get; }
}
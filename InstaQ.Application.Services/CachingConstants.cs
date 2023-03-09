namespace InstaQ.Application.Services;

internal static class CachingConstants
{
    public static string GetReportKey(Guid reportId) => $"report_{reportId}";
    public static string GetParticipantsKey(Guid userId) => $"participants_{userId}";
}
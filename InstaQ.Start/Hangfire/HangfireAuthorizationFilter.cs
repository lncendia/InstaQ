using Hangfire.Dashboard;

namespace InstaQ.Start.Hangfire;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => context.GetHttpContext().User.IsInRole("admin");
}
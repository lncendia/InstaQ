using InstaQ.WEB.Mappers.Abstractions;
using InstaQ.WEB.Mappers.ElementMapper;
using InstaQ.WEB.Mappers.ReportMapper;

namespace InstaQ.Start.Extensions;

internal static class WebServices
{
    internal static void AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IReportMapper, ReportMapper>();
        services.AddScoped<IElementMapper, ElementMapper>();
    }
}
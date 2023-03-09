using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Infrastructure.DataStorage;

namespace InstaQ.Start.Extensions;

internal static class DomainServices
{
    internal static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
using InstaQ.Application.Abstractions.Jobs.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Start.Exceptions;
using Microsoft.EntityFrameworkCore;
using InstaQ.Infrastructure.ApplicationDataStorage;
using InstaQ.Infrastructure.DataStorage;
using InstaQ.Infrastructure.DataStorage.Context;

namespace InstaQ.Start.Extensions;

internal static class PersistenceServices
{
    internal static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Main") ??
                               throw new ConfigurationException("ConnectionStrings:Main");
        var applicationConnectionString = configuration.GetConnectionString("Application") ??
                                          throw new ConfigurationException("ConnectionStrings:Application");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(applicationConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJobStorage, JobStorage>();
    }
}
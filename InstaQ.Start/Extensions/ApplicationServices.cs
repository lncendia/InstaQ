using InstaQ.Application.Abstractions.Elements.ServicesInterfaces;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.Links.ServicesInterfaces;
using InstaQ.Application.Abstractions.Participants.ServicesInterfaces;
using InstaQ.Application.Abstractions.Payments.ServicesInterfaces;
using InstaQ.Application.Abstractions.Profile.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Entities;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;
using InstaQ.Application.Services.Elements;
using InstaQ.Application.Services.Elements.Mappers;
using InstaQ.Application.Services.Links;
using InstaQ.Application.Services.Participants;
using InstaQ.Application.Services.Payments;
using InstaQ.Application.Services.Profile;
using InstaQ.Application.Services.ReportsManagement;
using InstaQ.Application.Services.ReportsProcessors.Initializers;
using InstaQ.Application.Services.ReportsProcessors.Processors;
using InstaQ.Application.Services.ReportsQuery;
using InstaQ.Application.Services.ReportsQuery.Mappers;
using InstaQ.Application.Services.Users;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using Microsoft.AspNetCore.Identity;

namespace InstaQ.Start.Extensions;

internal static class ApplicationServices
{
    internal static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILinkManager, LinkManager>();
        services.AddScoped<IParticipantManager, ParticipantManager>();
        services.AddScoped<IPaymentManager, PaymentManager>();
        services.AddScoped<IReportMapper, ReportMapper>();
        services.AddScoped<IReportManager, ReportManager>();
        services.AddScoped<IReportElementManager, ElementManager>();
        services.AddScoped<IElementMapper, ElementMapper>();
        services.AddScoped<IReportCreationService, ReportCreationService>();
        services.AddScoped<IReportStarter, StarterService>();
        services.AddScoped<IReportProcessorService, ProcessorService>();
        services.AddScoped<IReportInitializerService, ReportInitializerService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IUserParticipantsService, UserParticipantsService>();
        services.AddScoped<IUserLinksService, UserLinksService>();
        services.AddScoped<IParticipantMapper, ParticipantMapper>();
        services.AddScoped<IUsersManager, UsersManager>();

        var amount = configuration.GetValue<decimal>("Payments:RequestPrice");
        services.AddScoped<ISettingsService, SettingsService>(x =>
        {
            var manager = x.GetRequiredService<UserManager<UserData>>();
            var emailService = x.GetRequiredService<IEmailService>();
            var uow = x.GetRequiredService<IUnitOfWork>();
            var profileService = x.GetRequiredService<IInstagramProfileService>();
            return new SettingsService(manager, emailService, uow, profileService, amount);
        });
    }
}
using MediatR;
using InstaQ.Application.Services.Payments.EventHandlers;
using InstaQ.Application.Services.ReportsManagement.EventHandlers;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.BaseReport.Events;
using InstaQ.Domain.Reposts.ParticipantReport.Events;
using InstaQ.Domain.Transactions.Events;

namespace InstaQ.Start.Extensions;

internal static class EventsHandlers
{
    internal static void AddEventsHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(Program).Assembly);
        var amount = configuration.GetValue<decimal>("Payments:SubscribeCost");
        services.AddTransient<INotificationHandler<TransactionAcceptedEvent>, TransactionAcceptedDomainEventHandler>(
            x =>
            {
                var uow = x.GetRequiredService<IUnitOfWork>();
                return new TransactionAcceptedDomainEventHandler(uow, amount);
            });
        services
            .AddTransient<INotificationHandler<ParticipantReportFinishedEvent>,
                ParticipantReportFinishedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportCreatedEvent>, ReportCreatedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportFinishedEvent>, ReportFinishedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportDeletedEvent>, ReportDeletedDomainEventHandler>();
    }
}
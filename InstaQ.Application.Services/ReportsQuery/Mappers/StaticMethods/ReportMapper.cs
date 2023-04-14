using InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;
using InstaQ.Domain.Reposts.PublicationReport.Entities;

namespace InstaQ.Application.Services.ReportsQuery.Mappers.StaticMethods;

internal static class ReportMapper
{
    public static void InitReportBuilder(PublicationReportBuilder builder, PublicationReport report)
    {
        builder
            .WithHashtag(report.Hashtag)
            .WithId(report.Id)
            .WithCreationDate(report.CreationDate);
        if (report.AllParticipants) builder.WithAllParticipantsOption();

        if (!report.IsStarted) return;
        builder
            .WithProcess(report.Process)
            .WithPublicationsCount(report.Publications.Count)
            .WithDates(report.StartDate!.Value, report.EndDate)
            .WithStatus(report.IsCompleted, report.IsSucceeded)
            .WithRequests(report.RequestsCount);

        if (!string.IsNullOrEmpty(report.Message)) builder.WithMessage(report.Message);
    }
}
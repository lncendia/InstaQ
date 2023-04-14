using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;

namespace InstaQ.Application.Services.ReportsQuery.Mappers;

public class ParticipantReportMapper : IReportMapperUnit<ParticipantReportDto, ParticipantReport>
{
    public ParticipantReportDto Map(ParticipantReport report)
    {
        var builder = (ParticipantReportBuilder)ParticipantReportBuilder.ParticipantReportDto()
            .WithId(report.Id)
            .WithCreationDate(report.CreationDate)
            .WithElements(report.Participants.Count);
        if (!report.IsStarted) return builder.Build();
        builder
            .WithDates(report.StartDate!.Value, report.EndDate)
            .WithRequests(report.RequestsCount)
            .WithStatus(report.IsCompleted, report.IsSucceeded);
        if (!string.IsNullOrEmpty(report.Message)) builder.WithMessage(report.Message);
        return builder.Build();
    }
}
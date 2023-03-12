using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Domain.Users.Enums;

namespace InstaQ.Application.Services.ReportsProcessors.Processors;

public class ParticipantReportProcessor : IReportProcessorUnit<ParticipantReport>
{
    private readonly IParticipantsService _participantsGetterService;

    public ParticipantReportProcessor(IParticipantsService participantsGetterService)
    {
        _participantsGetterService = participantsGetterService;
    }

    public async Task ProcessReportAsync(ParticipantReport report, CancellationToken token)
    {
        if (!report.IsStarted) throw new ReportNotStartedException(report.Id);
        if (report.IsCompleted) throw new ReportAlreadyCompletedException(report.Id);

        var task = report.Type switch
        {
            ParticipantsType.Followers => _participantsGetterService.GetFollowersAsync(report.Pk, 500, token),
            ParticipantsType.Followings => _participantsGetterService.GetFollowingsAsync(report.Pk, 500, token),
            _ => throw new ArgumentOutOfRangeException()
        };
        var result = await task;
        foreach (var participantDto in result.Participants)
        {
            report.ProcessParticipantInfo(participantDto.Pk, participantDto.Name);
        }

        report.AddRequests(result.CountRequests);
    }
}
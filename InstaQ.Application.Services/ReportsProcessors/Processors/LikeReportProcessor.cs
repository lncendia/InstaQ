using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.LikeReport.DTOs;
using InstaQ.Domain.Reposts.LikeReport.Entities;

namespace InstaQ.Application.Services.ReportsProcessors.Processors;

public class LikeReportProcessor : IReportProcessorUnit<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILikesService _publicationInfoService;

    public LikeReportProcessor(IUnitOfWork unitOfWork, ILikesService publicationInfoService)
    {
        _unitOfWork = unitOfWork;
        _publicationInfoService = publicationInfoService;
    }

    public async Task ProcessReportAsync(LikeReport report, CancellationToken token)
    {
        int count = 0, i = report.Process;
        for (; i < report.Publications.Count; i++)
        {
            var publication = report.Publications.ElementAt(i);
            try
            {
                var result = await _publicationInfoService.GetAsync(publication.Pk, 500, token);
                report.SetLikes(new LikesDto(publication.Pk, result.Likes));
                report.AddRequests(result.CountRequests);
                count = 0;
            }
            catch (InstagramRequestException exception)
            {
                count++;
                report.SetLikes(new LikesDto(publication.Pk));
                if (count > 5) throw new TooManyRequestErrorsException(exception.Message);
            }

            if (i % 60 == 0) await SaveAsync(report);
            await Task.Delay(1000, token);
        }

        await SaveAsync(report);
    }

    private async Task SaveAsync(LikeReport report)
    {
        await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }
}
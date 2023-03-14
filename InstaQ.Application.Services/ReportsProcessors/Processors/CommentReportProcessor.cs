using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.CommentReport.DTOs;
using InstaQ.Domain.Reposts.CommentReport.Entities;

namespace InstaQ.Application.Services.ReportsProcessors.Processors;

public class CommentReportProcessor : IReportProcessorUnit<CommentReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentsService _publicationInfoService;

    public CommentReportProcessor(IUnitOfWork unitOfWork, ICommentsService publicationInfoService)
    {
        _unitOfWork = unitOfWork;
        _publicationInfoService = publicationInfoService;
    }

    public async Task ProcessReportAsync(CommentReport report, CancellationToken token)
    {
        int count = 0, i = report.Process;
        if (i % 50 == 0) await SaveAsync(report);
        for (; i < report.Publications.Count; i++)
        {
            var publication = report.Publications.ElementAt(i);
            try
            {
                var result = await _publicationInfoService.GetAsync(publication.Pk, 500, token);
                report.SetComments(new CommentsDto(publication.Pk, result.Comments));
                report.AddRequests(result.CountRequests);
                count = 0;
            }
            catch (InstagramRequestException ex)
            {
                report.SetComments(new CommentsDto(publication.Pk));
                count++;
                if (count > 5) throw new TooManyRequestErrorsException(ex.Message);
            }
            
            await Task.Delay(1000, token);
        }

        await SaveAsync(report);
    }

    private async Task SaveAsync(CommentReport report)
    {
        await _unitOfWork.CommentReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }
}
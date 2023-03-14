using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.PublicationReport.Exceptions;
using Microsoft.Extensions.Logging;

namespace InstaQ.Application.Services.ReportsManagement;

public class StarterService : IReportStarter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportProcessorService _processorService;
    private readonly IReportInitializerService _initializerService;
    private readonly ILogger<StarterService> _logger;

    public StarterService(IUnitOfWork unitOfWork, IReportInitializerService initializerService,
        IReportProcessorService processorService, ILogger<StarterService> logger)
    {
        _unitOfWork = unitOfWork;
        _initializerService = initializerService;
        _processorService = processorService;
        _logger = logger;
    }

    public async Task StartLikeReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            if (!report.IsStarted)
                await _initializerService.LikeReportInitializer.Value.InitializeReportAsync(report, token);
            await _processorService.LikeReportProcessor.Value.ProcessReportAsync(report, token);
            report.Finish();
        }
        catch (Exception e)
        {
            report.Finish(HandleException(e, id));
        }

        await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task StartCommentReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.CommentReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            if (!report.IsStarted)
                await _initializerService.CommentReportInitializer.Value.InitializeReportAsync(report, token);
            await _processorService.CommentReportProcessor.Value.ProcessReportAsync(report, token);
            report.Finish();
        }
        catch (Exception e)
        {
            report.Finish(HandleException(e, id));
        }

        await _unitOfWork.CommentReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task StartParticipantReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            if (!report.IsStarted)
                await _initializerService.ParticipantReportInitializer.Value.InitializeReportAsync(report, token);
            await _processorService.ParticipantReportProcessor.Value.ProcessReportAsync(report, token);
            report.Finish();
        }
        catch (Exception e)
        {
            report.Finish(HandleException(e, id));
        }

        await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private string HandleException(Exception ex, Guid id)
    {
        switch (ex)
        {
            case ReportAlreadyCompletedException:
                return "Отчёт уже является сформированным";
            case InstagramRequestException:
                return $"Ошибка Instagram: {ex.Message}";
            case ReportNotStartedException:
                return "Отчёт не инициализован";
            case TooManyRequestErrorsException:
                return $"Не удается получить информацию. {ex.Message}";
            case UserNotFoundException:
                return "Создатель отчёта не найден";
            case ElementsListEmptyException:
                return "Участники не найдены";
            case PublicationsListEmptyException:
                return "Публикации по хештегу не найдены";
            case HttpRequestException httpEx:
                if (httpEx.InnerException is OperationCanceledException) throw httpEx.InnerException;
                _logger.LogError(ex.InnerException, "Report {Id} completed with an error", id);
                return "Возникла ошибка с подключением на сервере";
            default:
                if (ex is not OperationCanceledException)
                    _logger.LogError(ex, "Report {Id} completed with an error", id);
                throw ex;
        }
    }
}
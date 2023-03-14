using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.PublicationReport.Exceptions;

namespace InstaQ.Application.Services.ReportsManagement;

public class StarterService : IReportStarter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportProcessorService _processorService;
    private readonly IReportInitializerService _initializerService;

    public StarterService(IUnitOfWork unitOfWork, IReportInitializerService initializerService,
        IReportProcessorService processorService)
    {
        _unitOfWork = unitOfWork;
        _initializerService = initializerService;
        _processorService = processorService;
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
            report.Finish(HandleException(e));
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
            report.Finish(HandleException(e));
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
            report.Finish(HandleException(e));
        }

        await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private static string HandleException(Exception ex)
    {
        return ex switch
        {
            ReportAlreadyCompletedException => "Отчёт уже является сформированным",
            InstagramRequestException => $"Ошибка Instagram: {ex.Message}",
            ReportNotStartedException => "Отчёт не инициализован",
            TooManyRequestErrorsException => $"Не удается получить информацию. {ex.Message}",
            UserNotFoundException => "Создатель отчёта не найден",
            ElementsListEmptyException => "Участники не найдены",
            PublicationsListEmptyException => "Публикации по хештегу не найдены",
            HttpRequestException httpEx => httpEx.InnerException is TaskCanceledException
                ? throw httpEx.InnerException
                : "Возникла ошибка с подключением на сервере",
            _ => throw ex
        };
    }
}
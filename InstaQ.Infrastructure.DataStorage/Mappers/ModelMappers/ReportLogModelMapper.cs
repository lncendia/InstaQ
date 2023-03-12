using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class LogModelMapper : IModelMapperUnit<LogModel, ReportLog>
{
    private readonly ApplicationDbContext _context;

    public LogModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<LogModel> MapAsync(ReportLog model)
    {
        var reportLog = await _context.Logs.FirstOrDefaultAsync(x => x.Id == model.Id) ??
                        new LogModel { Id = model.Id };
        reportLog.ReportId = model.ReportId;
        reportLog.UserId = model.UserId;
        reportLog.Success = model.Success;
        reportLog.CreatedAt = model.CreatedAt;
        reportLog.FinishedAt = model.FinishedAt;
        reportLog.AdditionalInfo = model.AdditionalInfo;
        reportLog.Type = model.Type;
        reportLog.Amount = model.Amount;
        return reportLog;
    }
}
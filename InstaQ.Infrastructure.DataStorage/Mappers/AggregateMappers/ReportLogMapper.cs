using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class LogMapper : IAggregateMapperUnit<ReportLog, LogModel>
{
    public ReportLog Map(LogModel model)
    {
        var reportLog = new ReportLog(model.UserId, model.ReportId ?? Guid.Empty, model.Type, model.CreatedAt,
            model.AdditionalInfo);
        IdFields.AggregateId.SetValue(reportLog, model.Id);
        if (model.FinishedAt.HasValue)
        {
            reportLog.Finish(model.Success!.Value, model.Amount!.Value, model.FinishedAt.Value);
        }

        if (!model.ReportId.HasValue) reportLog.ReportDeleted();

        return reportLog;
    }
}
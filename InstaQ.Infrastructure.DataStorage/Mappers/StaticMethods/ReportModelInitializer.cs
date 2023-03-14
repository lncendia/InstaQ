using InstaQ.Domain.Reposts.BaseReport.Entities;
using InstaQ.Domain.Reposts.PublicationReport.Entities;
using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;
using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;

internal static class ModelInitializer
{
    internal static void InitPublicationReportModel(PublicationReportModel report, PublicationReport element)
    {
        if (!report.Publications.Any())
        {
            report.Publications = element.Publications.Select(x => new PublicationModel
                {EntityId = x.Id, OwnerPk = x.OwnerPk, Pk = x.Pk, IsLoaded = x.IsLoaded, Code = x.Code}).ToList();
        }
        else
        {
            var elementPublications = element.Publications.OrderBy(x => x.Id).ToList();
            var reportPublications = report.Publications.OrderBy(x => x.Id).ToList();
            for (var i = 0; i < elementPublications.Count; i++)
                reportPublications[i].IsLoaded = elementPublications[i].IsLoaded;
        }

        report.Hashtag = element.Hashtag;
        report.Process = element.Process;
        report.AllParticipants = element.AllParticipants;
        report.CountPublicationsToGet = element.CountPublicationsToGet;
        InitReportModel(report, element);
    }

    internal static void InitReportModel(ReportModel report, Report element)
    {
        report.Id = element.Id;
        report.Message = element.Message;
        report.UserId = element.UserId;
        report.CreationDate = element.CreationDate;
        report.EndDate = element.EndDate;
        report.IsSucceeded = element.IsSucceeded;
        report.StartDate = element.StartDate;
        report.CountRequests = element.RequestsCount;
    }
}
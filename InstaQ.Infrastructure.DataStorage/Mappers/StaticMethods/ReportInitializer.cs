using System.Reflection;
using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;
using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;
using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Reposts.BaseReport.Entities;
using InstaQ.Domain.Reposts.PublicationReport.Entities;

namespace InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;

internal static class Initializer
{
    private static readonly Type PublicationReportType = typeof(PublicationReport);
    private static readonly Type ReportType = PublicationReportType.BaseType!;
    private static readonly Type PublicationType = typeof(Publication);

    private static readonly FieldInfo ReportUserId =
        ReportType.GetField("<UserId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportCountPublicationsToGet =
        PublicationReportType.GetField("<CountPublicationsToGet>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportCreationDate =
        ReportType.GetField("<CreationDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportStartDate =
        ReportType.GetField("<StartDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportEndDate =
        ReportType.GetField("<EndDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportSucceeded =
        ReportType.GetField("<IsSucceeded>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportMessage =
        ReportType.GetField("<Message>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportCountRequests =
        ReportType.GetField("<RequestsCount>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportElementsList =
        ReportType.GetField("ReportElementsList", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportLinkedUsersList =
        PublicationReportType.GetField("_linkedUsersList", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportHashtag =
        PublicationReportType.GetField("<Hashtag>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportPublicationsList =
        PublicationReportType.GetField("PublicationsList", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportProcess =
        PublicationReportType.GetField("<Process>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportAllParticipants =
        PublicationReportType.GetField("<AllParticipants>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo PublicationIsLoaded =
        typeof(Publication).GetField("<IsLoaded>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    internal static void InitPublicationReport(PublicationReport report, IEnumerable<PublicationReportElement> elements,
        PublicationReportModel model)
    {
        ReportAllParticipants.SetValue(report, model.AllParticipants);
        ReportLinkedUsersList.SetValue(report, model.LinkedUsers.Select(x => x.Id).ToList());
        ReportHashtag.SetValue(report, model.Hashtag);
        ReportPublicationsList.SetValue(report, model.Publications.Select(GetPublication).ToList());
        ReportProcess.SetValue(report, model.Process);
        ReportCountPublicationsToGet.SetValue(report, model.CountPublicationsToGet);
        InitReport(report, elements, model);
    }

    private static Publication GetPublication(PublicationModel model)
    {
        object?[] args = {model.Pk, model.OwnerPk, model.Code, model.EntityId};
        var publication = (Publication) PublicationType.Assembly.CreateInstance(
            PublicationType.FullName!, false, BindingFlags.Instance | BindingFlags.NonPublic, null, args!,
            null, null)!;
        PublicationIsLoaded.SetValue(publication, model.IsLoaded);
        return publication;
    }

    internal static void InitReport(Report report, IEnumerable<ReportElement> elements, ReportModel reportModel)
    {
        IdFields.AggregateId.SetValue(report, reportModel.Id);
        ReportUserId.SetValue(report, reportModel.UserId);
        ReportCreationDate.SetValue(report, reportModel.CreationDate);
        ReportStartDate.SetValue(report, reportModel.StartDate);
        ReportEndDate.SetValue(report, reportModel.EndDate);
        ReportSucceeded.SetValue(report, reportModel.IsSucceeded);
        ReportMessage.SetValue(report, reportModel.Message);
        ReportElementsList.SetValue(report, elements.ToList());
        ReportCountRequests.SetValue(report, reportModel.CountRequests);
        IdFields.Events.SetValue(report, new List<IDomainEvent>());
    }
}
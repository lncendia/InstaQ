using System.Reflection;
using System.Runtime.Serialization;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ParticipantReportMapper : IAggregateMapperUnit<ParticipantReport, ParticipantReportModel>
{
    private static readonly Type ParticipantReportType = typeof(ParticipantReport);
    private static readonly Type ParticipantReportElementType = typeof(ParticipantReportElement);

    private static readonly FieldInfo Pk =
        ParticipantReportType.GetField("<Pk>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ParticipantsType =
        ParticipantReportType.GetField("<Type>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;
    
    private static readonly FieldInfo Type =
        ParticipantReportElementType.GetField("<Type>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo NewName =
        ParticipantReportElementType.GetField("<NewName>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    public ParticipantReport Map(ParticipantReportModel model)
    {
        var report = (ParticipantReport)FormatterServices.GetUninitializedObject(ParticipantReportType);
        var elementsGrouping = model.ElementsList.GroupBy(x => x.OwnerId).ToList();
        var elements = new List<ParticipantReportElement>();
        if (elementsGrouping.Any())
        {
            foreach (var group in elementsGrouping.First(x => x.Key == null))
            {
                var parent = GetParticipantElement(group, null);
                elements.Add(parent);
                var children = elementsGrouping.FirstOrDefault(x => x.Key == group.EntityId);
                if (children == null) continue;
                elements.AddRange(children.Select(x => GetParticipantElement(x, parent)));
            }
        }

        Initializer.InitReport(report, elements, model);
        Pk.SetValue(report, model.Pk);
        ParticipantsType.SetValue(report, model.Type);
        return report;
    }

    private static ParticipantReportElement GetParticipantElement(ParticipantReportElementModel model,
        ParticipantReportElement? owner)
    {
        object?[] args = { model.Name, model.Pk, model.ParticipantId, owner, model.EntityId };
        var element = (ParticipantReportElement)ParticipantReportElementType.Assembly.CreateInstance(
            ParticipantReportElementType.FullName!, false, BindingFlags.Instance | BindingFlags.NonPublic, null, args!,
            null, null)!;
        Type.SetValue(element, model.Type);
        NewName.SetValue(element, model.NewName);
        return element;
    }
}
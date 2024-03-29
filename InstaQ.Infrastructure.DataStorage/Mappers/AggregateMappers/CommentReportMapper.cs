﻿using System.Reflection;
using System.Runtime.Serialization;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.CommentReport.ValueObjects;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models.Reports.CommentReport;

namespace InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class CommentReportMapper : IAggregateMapperUnit<CommentReport, CommentReportModel>
{
    private static readonly Type CommentReportType = typeof(CommentReport);
    private static readonly Type CommentReportElementType = typeof(CommentReportElement);
    private static readonly Type CommentInfoElementType = typeof(CommentInfo);

    private static readonly FieldInfo ElementComments =
        CommentReportElementType.GetField("_comments", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public CommentReport Map(CommentReportModel model)
    {
        var report = (CommentReport)FormatterServices.GetUninitializedObject(CommentReportType);
        var elementsGrouping = model.ElementsList.GroupBy(x => x.OwnerId).ToList();
        var elements = new List<CommentReportElement>();

        if (elementsGrouping.Any())
        {
            foreach (var group in elementsGrouping.First(x => x.Key == null))
            {
                var parent = GetCommentElement(group, null);
                elements.Add(parent);
                var children = elementsGrouping.FirstOrDefault(x => x.Key == group.EntityId);
                if (children == null) continue;
                elements.AddRange(children.Select(x => GetCommentElement(x, parent)));
            }
        }

        Initializer.InitPublicationReport(report, elements, model);
        return report;
    }

    private static CommentReportElement GetCommentElement(CommentReportElementModel model, CommentReportElement? owner)
    {
        object?[] args =
        {
            model.Name, model.LikeChatName, model.Pk, model.ParticipantId, model.Vip, model.Note, owner,
            model.EntityId
        };
        var element = (CommentReportElement)CommentReportElementType.Assembly.CreateInstance(
            CommentReportElementType.FullName!, false, BindingFlags.Instance | BindingFlags.NonPublic, null, args!,
            null, null)!;
        ElementComments.SetValue(element, GetCommentsInfo(model.Comments));
        if (model.IsAccepted) element.Accept();
        return element;
    }

    private static List<CommentInfo> GetCommentsInfo(string rawString)
    {
        if (string.IsNullOrEmpty(rawString)) return new List<CommentInfo>();
        var data = rawString.Split(';');
        var list = new List<CommentInfo>();
        foreach (var value in data)
        {
            var commentInfo = value.Split(':', 3);
            var id = int.Parse(commentInfo[0]);
            var confirmed = int.Parse(commentInfo[1]) == 1;
            var comment = commentInfo[2];
            if (string.IsNullOrWhiteSpace(comment)) comment = null;
            object?[] args = { id, confirmed, comment };
            var element = (CommentInfo)CommentInfoElementType.Assembly.CreateInstance(
                CommentInfoElementType.FullName!, false, BindingFlags.Instance | BindingFlags.NonPublic, null, args!,
                null, null)!;
            list.Add(element);
        }

        return list;
    }
}
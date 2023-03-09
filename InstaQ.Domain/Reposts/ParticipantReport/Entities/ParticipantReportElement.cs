using InstaQ.Domain.Reposts.BaseReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Enums;

namespace InstaQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReportElement : ReportElement
{
    internal ParticipantReportElement(string name, string pk, Guid? participantId, ParticipantReportElement? parent,
        int id) : base(name, pk, id)
    {
        Parent = parent;
        ParticipantId = participantId;
    }

    public ParticipantReportElement? Parent { get; }
    public Guid? ParticipantId { get; }
    public string? NewName { get; private set; }
    public ElementType? Type { get; private set; }

    internal void SetType(ElementType type, string? newName = null)
    {
        if (type != ElementType.New && !ParticipantId.HasValue)
            throw new InvalidOperationException("ParticipantId is null");

        if (type == ElementType.Rename && string.IsNullOrEmpty(newName))
            throw new ArgumentException("New name is required for rename element");

        Type = type;
        NewName = newName;
    }
}
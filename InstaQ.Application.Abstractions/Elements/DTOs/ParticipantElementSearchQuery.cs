using InstaQ.Domain.Reposts.ParticipantReport.Enums;

namespace InstaQ.Application.Abstractions.Elements.DTOs;

public class ParticipantElementSearchQuery
{
    public ParticipantElementSearchQuery(int page, string? name, ElementType? elementType, bool? hasChildren)
    {
        Page = page;
        NameNormalized = name?.ToUpper();
        ElementType = elementType;
        HasChildren = hasChildren;
    }

    public int Page { get; }
    public string? NameNormalized { get; }
    public ElementType? ElementType { get; }
    public bool? HasChildren { get; }
}
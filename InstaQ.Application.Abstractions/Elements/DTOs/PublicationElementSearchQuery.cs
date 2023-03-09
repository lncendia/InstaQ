namespace InstaQ.Application.Abstractions.Elements.DTOs;

public class PublicationElementSearchQuery
{
    public PublicationElementSearchQuery(int page, string? name, bool? succeeded, string? likeChatName, bool? hasChildren, bool? vip)
    {
        Page = page;
        NameNormalized = name?.ToUpper();
        Succeeded = succeeded;
        LikeChatName = likeChatName;
        HasChildren = hasChildren;
        Vip = vip;
    }

    public int Page { get; }
    public string? NameNormalized { get; }
    public bool? Succeeded { get; }
    public string? LikeChatName { get; }
    public bool? HasChildren { get; }
    public bool? Vip { get; }
}
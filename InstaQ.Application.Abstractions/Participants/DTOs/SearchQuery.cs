namespace InstaQ.Application.Abstractions.Participants.DTOs;

public class SearchQuery
{
    public SearchQuery(int page, string? name = null, bool? vip = null, bool? hasChildren = null)
    {
        Page = page;
        NameNormalized = name?.ToUpper();
        Vip = vip;
        HasChildren = hasChildren;
    }

    public int Page { get; }
    public string? NameNormalized { get; }
    public bool? Vip { get; }
    public bool? HasChildren { get; }
}
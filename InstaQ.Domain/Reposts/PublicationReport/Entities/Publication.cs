using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.PublicationReport.Entities;

public class Publication : Entity
{
    internal Publication(string itemId, string pk, int id) : base(id)
    {
        ItemId = itemId;
        Pk = pk;
    }

    public string ItemId { get; }
    public string Pk { get; }
    public bool? IsLoaded { get; internal set; }
}
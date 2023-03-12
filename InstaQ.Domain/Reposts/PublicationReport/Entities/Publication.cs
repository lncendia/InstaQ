using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.PublicationReport.Entities;

public class Publication : Entity
{
    internal Publication(string pk, string ownerPk, string code, int id) : base(id)
    {
        OwnerPk = ownerPk;
        Code = code;
        Pk = pk;
    }

    public string OwnerPk { get; }
    public string Pk { get; }
    public string Code { get; }
    public bool? IsLoaded { get; internal set; }
}
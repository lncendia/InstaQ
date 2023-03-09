using InstaQ.Domain.Abstractions;

namespace InstaQ.Domain.Reposts.BaseReport.Entities;

public abstract class ReportElement : Entity
{
    private protected ReportElement(string name, string pk, int id) : base(id)
    {
        Name = name;
        Pk = pk;
    }

    public string Name { get; }
    public string Pk { get; }
}
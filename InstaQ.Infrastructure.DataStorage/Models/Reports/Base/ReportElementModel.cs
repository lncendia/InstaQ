using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.Base;

public abstract class ElementModel : IEntityModel
{
    public int Id { get; set; }
    public int EntityId { get; set; }
    public string Name { get; set; } = null!;
    public string Pk { get; set; } = null!;
}
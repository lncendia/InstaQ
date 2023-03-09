using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models;

public class ParticipantModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Notes { get; set; }
    public bool Vip { get; set; }
    public string Pk { get; set; } = null!;
    public Guid? ParentParticipantId { get; set; }
    public ParticipantModel? ParentParticipant { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
}
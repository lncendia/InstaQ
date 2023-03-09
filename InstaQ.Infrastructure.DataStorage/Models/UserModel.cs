using InstaQ.Domain.Users.Enums;
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models;

public class UserModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset? SubscriptionDate { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public string? TargetPk { get; set; }
    public string? TargetUsername { get; set; }
    public ParticipantsType? ParticipantsType { get; set; }
    public DateTimeOffset? TargetSetTime { get; set; }
}
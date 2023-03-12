using System.ComponentModel.DataAnnotations.Schema;
using InstaQ.Domain.Users.Enums;
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models;

public class UserModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [Column(TypeName = "money")] public decimal Balance { get; set; }
    public string? TargetPk { get; set; }
    public string? TargetUsername { get; set; }
    public ParticipantsType? ParticipantsType { get; set; }
}
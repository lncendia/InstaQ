using System.ComponentModel.DataAnnotations.Schema;
using InstaQ.Domain.ReportLogs.Enums;
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models;

public class LogModel : IAggregateModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public Guid? ReportId { get; set; }
    public ReportType Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public bool? Success { get; set; }
    public string AdditionalInfo { get; set; } = null!;
    [Column(TypeName = "money")] public decimal? Amount { get; set; }
}
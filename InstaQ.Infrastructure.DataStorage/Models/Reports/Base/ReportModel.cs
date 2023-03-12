using System.ComponentModel.DataAnnotations.Schema;
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models.Reports.Base;

public abstract class ReportModel : IAggregateModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public bool IsSucceeded { get; set; }
    public string? Message { get; set; }
    public int CountRequests { get; set; }
}
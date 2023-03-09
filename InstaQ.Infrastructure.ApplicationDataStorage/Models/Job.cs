using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaQ.Infrastructure.ApplicationDataStorage.Models;

public class Job
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string JobId { get; set; } = null!;

    public Guid ReportId { get; set; }
}
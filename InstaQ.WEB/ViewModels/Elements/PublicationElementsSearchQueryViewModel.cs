using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Elements;

public class PublicationElementsSearchQueryViewModel
{
    public int Page { get; set; } = 1;
    [Required] public Guid ReportId { get; set; }
    [StringLength(50)] public string? Name { get; set; }
    public bool? Succeeded { get; set; }
    [StringLength(50)] public string? LikeChatName { get; set; }
    public bool? HasChildren { get; set; }
    public bool? Vip { get; set; }
}
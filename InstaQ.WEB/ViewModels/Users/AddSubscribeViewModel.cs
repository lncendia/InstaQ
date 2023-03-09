using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Users;

public class AddSubscribeViewModel
{
    [Required] public Guid Id { get; set; }
    
    [Display(Name = "На сколько дней продлить подписку")]
    public int CountDays { get; set; }
}
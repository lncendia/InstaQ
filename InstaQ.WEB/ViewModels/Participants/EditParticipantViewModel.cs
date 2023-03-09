using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Participants;

public class EditParticipantViewModel
{
    public string? Username { get; set; }

    [Required(ErrorMessage = "Поле не должно быть пустым")]
    public Guid Id { get; set; }

    [Display(Name = "Заметка")]
    [StringLength(500, ErrorMessage = "Не больше 500 символов")]
    public string? Note { get; set; }

    [Display(Name = "VIP")] public bool Vip { get; set; }

    [Display(Name = "Родительский аккаунт")]
    public Guid? ParentId { get; set; }
}
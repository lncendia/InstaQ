using System.ComponentModel.DataAnnotations;
using InstaQ.Domain.Users.Enums;

namespace InstaQ.WEB.ViewModels.Settings;

public class ChangeTargetViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Введите имя цели")]
    public string Target { get; set; } = null!;

    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Выберите тип")]
    public ParticipantsType Type { get; set; }
}
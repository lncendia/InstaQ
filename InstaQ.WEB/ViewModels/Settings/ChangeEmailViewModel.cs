using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Settings;

public class ChangeEmailViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Введите корректный адрес")]
    [Display(Name = "Введите новый электронный адрес")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string Email { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Users;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите электронный адрес")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    [DataType(DataType.Password)]
    [Display(Name = "Новый пароль")]
    public string Password { get; set; } = null!;
}
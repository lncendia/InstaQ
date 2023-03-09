using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Users;

public class EditUserViewModel
{
    [Required] public Guid Id { get; set; }

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите электронный адрес")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string? NewEmail { get; set; }

    [RegularExpression("^[a-zA-Zа-яА-Я0-9_ ]{3,20}$", ErrorMessage = "Имя пользователя должно содержать от 3 до 20 символов")]
    [Display(Name = "Введите имя пользователя")]
    public string? Username { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Links;

public class CreateLinkViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите новый электронный адрес")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string Email { get; set; } = null!;
}
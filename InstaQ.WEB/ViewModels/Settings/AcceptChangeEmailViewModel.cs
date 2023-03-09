using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Settings;

public class AcceptChangeEmailViewModel
{
    [Required(ErrorMessage = "Не указана почта")]
    [DataType(DataType.EmailAddress)]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Не указан код подтверждения")]
    public string Code { get; set; } = null!;
}
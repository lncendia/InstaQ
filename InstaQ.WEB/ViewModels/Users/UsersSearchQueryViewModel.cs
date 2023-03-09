using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Users;

public class UsersSearchQueryViewModel
{
    [RegularExpression("^[a-zA-Zа-яА-Я0-9_ ]{3,20}$")]
    public string? Username { get; set; }

    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    public int Page { get; set; } = 1;
}
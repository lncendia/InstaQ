using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Users;

public class ChangeBalanceViewModel
{
    [Required] public Guid Id { get; set; }

    [Display(Name = "Введите новый баланс")]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }
}
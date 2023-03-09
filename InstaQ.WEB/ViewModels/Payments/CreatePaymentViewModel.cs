using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Payments;

public class CreatePaymentViewModel
{
    [Required(ErrorMessage = "Не указана сумма платежа")]
    [Display(Name = "Сумма оплаты")]
    [Range(1, 10000, ErrorMessage = "Сумма должна быть больше 0 и меньше 10000 рублей")]
    [DataType(DataType.Currency, ErrorMessage = "Неверный формат данных")]
    public int Amount { get; set; }
}
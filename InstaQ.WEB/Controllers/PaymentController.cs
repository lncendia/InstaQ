using System.Security.Claims;
using InstaQ.WEB.ViewModels.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Payments.Exceptions;
using InstaQ.Application.Abstractions.Payments.ServicesInterfaces;
using InstaQ.Domain.Transactions.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly IPaymentManager _paymentService;
    public PaymentController(IPaymentManager paymentService) => _paymentService = paymentService;


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePaymentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var payment = await _paymentService.CreateAsync(userId, model.Amount);
            return Json(new PaymentViewModel(payment.Amount, payment.CreationDate, payment.PayUrl, payment.Id));
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                TransactionNotFoundException => "Счёт не найден",
                BillNotPaidException => "Счёт не оплачен",
                TransactionAlreadyAcceptedException => "Счёт уже подтверждён",
                ErrorCheckBillException => "Ошибка при отправке запроса на проверку оплаты",
                _ => "Произошла ошибка при выставлении счёта"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Check(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _paymentService.ConfirmAsync(userId, id.Value);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                TransactionNotFoundException => "Счёт не найден",
                BillNotPaidException => "Счёт не оплачен",
                TransactionAlreadyAcceptedException => "Счёт уже подтверждён",
                ErrorCheckBillException => "Ошибка при отправке запроса на проверку оплаты",
                _ => "Произошла ошибка при проверке оплаты"
            };
            return BadRequest(text);
        }
    }
}
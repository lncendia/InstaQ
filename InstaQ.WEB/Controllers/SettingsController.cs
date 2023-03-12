using System.Security.Claims;
using InstaQ.Application.Abstractions.Profile.Exceptions;
using InstaQ.WEB.ViewModels.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Profile.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Users.Enums;
using InstaQ.Domain.Users.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly ISettingsService _userService;

    public SettingsController(ISettingsService userService)
    {
        _userService = userService;
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.RequestResetEmailAsync(userId, model.Email, Url.Action(
                "AcceptChangeEmail", "Settings", null, HttpContext.Request.Scheme)!);

            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                EmailException => "Произошла ошибка при отправке письма",
                UserAlreadyExistException => "Пользователь с такой почтой уже зарегистрирован",
                ArgumentException => "Некорректные данные",
                _ => "Произошла ошибка при изменении почты"
            };

            return BadRequest(text);
        }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptChangeEmail(AcceptChangeEmailViewModel model)
    {
        if (!ModelState.IsValid) return RedirectToAction("ChangeEmail", new { message = "Ссылка недействительна" });

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.ResetEmailAsync(userId, model.Email, model.Code);
            await ChangeClaimAsync(ClaimTypes.Email, model.Email);
            return RedirectToAction("Index", "Home", new { message = "Почта успешно изменена" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                InvalidCodeException => "Ссылка недействительна",
                UserAlreadyExistException => "Пользователь с такой почтой уже зарегистрирован",
                InvalidEmailException => "Неверный формат почты",
                _ => "Произошла ошибка при смене почты"
            };

            return RedirectToAction("Index", "Home", new { message = text });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _userService.ChangePasswordAsync(email, model.OldPassword,
                model.Password);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                ArgumentException => "Некорректные данные",
                _ => "Произошла ошибка при изменении пароля"
            };

            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeName(ChangeNameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.ChangeNameAsync(userId, model.Name);
            await ChangeClaimAsync(ClaimTypes.Name, model.Name);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InvalidNicknameException => "Некорректные данные",
                _ => "Произошла ошибка при изменении имени"
            };

            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeTarget(ChangeTargetViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.ChangeTargetAsync(userId, model.Target, model.Type);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InstagramNotFoundException => "Инстаграм не найден",
                ProfilePrivateException => "Профиль цели должен быть открыт",
                InsufficientFundsException => "Недостаточно средств",
                ProfileEmptyException exception =>
                    $"У пользователя отстутствуют {(exception.Type == ParticipantsType.Followers ? "подписчики" : "подписки")}",
                _ => "Произошла ошибка при смене цели"
            };

            return BadRequest(text);
        }
    }

    private async Task ChangeClaimAsync(string key, string value)
    {
        if (HttpContext.User.Identity is ClaimsIdentity claimsIdentity)
        {
            var claim = claimsIdentity.FindFirst(key);
            if (claimsIdentity.TryRemoveClaim(claim))
            {
                claimsIdentity.AddClaim(new Claim(key, value));
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);
            }
        }
    }
}
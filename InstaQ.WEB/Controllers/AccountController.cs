﻿using InstaQ.WEB.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Users.DTOs;
using InstaQ.Application.Abstractions.Users.Entities;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;
using InstaQ.Domain.Users.Exceptions;

namespace InstaQ.WEB.Controllers;

public class AccountController : Controller
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly SignInManager<UserData> _signInManager;

    public AccountController(IUserAuthenticationService userAuthenticationService,
        SignInManager<UserData> signInManager)
    {
        _userAuthenticationService = userAuthenticationService;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            await _userAuthenticationService.CreateAsync(new UserCreateDto(model.Name, model.Password, model.Email),
                Url.Action("AcceptCode", "Account", null, HttpContext.Request.Scheme)!);

            return RedirectToAction("Index", "Home",
                new { message = "Завершите регистрацию, перейдя по ссылке, отправленной вам на почту" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserAlreadyExistException => "Пользователь с такой почтой уже зарегистрирован",
                InvalidEmailException => "Неверный формат почты",
                InvalidNicknameException => "Неверный формат имени пользователя",
                EmailException => "Не удалось отправить сообщение вам на почту",
                UserCreationException => ex.Message,
                _ => "Произошла ошибка при регистрации"
            };
            ModelState.AddModelError("", text);
            return View(model);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> AcceptCode(string? email, string? code)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Index", "Home",
                new { message = "Ссылка недействительна" });
        }

        try
        {
            var user = await _userAuthenticationService.CodeAuthenticateAsync(email, code);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home",
                new { message = "Почта успешно подтверждена" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                InvalidCodeException => "Ссылка недействительна",
                _ => "Произошла ошибка при подтверждении почты"
            };

            return RedirectToAction("Index", "Home",
                new { message = text });
        }
    }

    public IActionResult LoginOauth(string provider, string returnUrl = "/")
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null) return RedirectToAction(nameof(Login));

        try
        {
            var user = await _userAuthenticationService.ExternalAuthenticateAsync(info);
            await _signInManager.SignInAsync(user, true);
            await HttpContext.SignOutAsync(info.AuthenticationProperties);
            return Redirect(returnUrl);
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserAlreadyExistException => "Пользователь с таким логином уже существует",
                InvalidEmailException => "Неверный формат почты",
                InvalidNicknameException => "Неверный формат имени пользователя",
                UserCreationException => ex.Message,
                _ => "Произошла ошибка при входе"
            };
            return RedirectToAction("Login", "Account", new { message = text });
        }
    }

    [HttpGet]
    public IActionResult Login(string message, string returnUrl = "/")
    {
        ViewData["Alert"] = message;
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            var user = await _userAuthenticationService.AuthenticateAsync(model.Email, model.Password);
            await _signInManager.SignInAsync(user, model.RememberMe);
            return Redirect(model.ReturnUrl);
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                InvalidPasswordException => "Неверный пароль",
                _ => "Произошла ошибка при входе"
            };
            ModelState.AddModelError("", text);
            return View(model);
        }
    }

    [Authorize(Policy = "Identity.Application")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home",
            new { message = "Вы вышли из аккаунта." });
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            await _userAuthenticationService.RequestResetPasswordAsync(model.Email, Url.Action(
                "NewPassword",
                "Account", null,
                HttpContext.Request.Scheme)!);
            return RedirectToAction("Login", new
            {
                message =
                    "Для восстановления пароля проверьте электронную почту и перейдите по ссылке, указанной в письме."
            });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                EmailException => "Произошла ошибка при отправке письма",
                _ => "Произошла ошибка при восстановлении пароля"
            };
            ModelState.AddModelError("", text);

            return View(model);
        }
    }

    [HttpGet]
    public IActionResult NewPassword(string? email, string? code)
    {
        if (email is null && code is null)
        {
            return RedirectToAction("Login", new { message = "Ссылка недействительна" });
        }

        return View(new EnterNewPasswordViewModel { Email = email!, Code = code! });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> NewPassword(EnterNewPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            await _userAuthenticationService.ResetPasswordAsync(model.Email, model.Code, model.Password);
            return RedirectToAction("Login", new { message = "Пароль успешно изменен" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                InvalidCodeException => "Ссылка недействительна",
                _ => "Произошла ошибка при восстановлении пароля"
            };
            ModelState.AddModelError("", text);


            return View(model);
        }
    }
}
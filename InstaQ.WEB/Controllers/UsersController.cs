using InstaQ.WEB.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Users.DTOs;
using InstaQ.Application.Abstractions.Users.Entities;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;
using InstaQ.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace InstaQ.WEB.Controllers;

[Authorize(Roles = "admin")]
public class UsersController : Controller
{
    private readonly IUsersManager _usersManager;
    private readonly SignInManager<UserData> _signInManager;

    public UsersController(IUsersManager usersManager, SignInManager<UserData> signInManager)
    {
        _usersManager = usersManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Users(UsersSearchQueryViewModel model)
    {
        var users = await _usersManager.FindAsync(new SearchQuery(model.Page, model.Username, model.Email));
        return users.Any()
            ? PartialView("Users", users.Select(x => new UserShortViewModel(x.Id, x.Username, x.Email)))
            : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? userId)
    {
        if (!userId.HasValue) return RedirectToAction("Index", new { message = "Неверный идентификатор" });

        try
        {
            var user = await _usersManager.GetAsync(userId.Value);
            return View(new UserViewModel(user.Id, user.Username, user.Email, user.Balance));
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка при получении пользователя"
            };
            return RedirectToAction("Index", new { message = text });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            await _usersManager.EditAsync(new EditUserDto(model.Id, model.Username, model.NewEmail));
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                UserAlreadyExistException => "Пользователь с такой почтой уже зарегистрирован",
                InvalidEmailException => "Неверный формат почты",
                InvalidNicknameException => "Неверный формат имени пользователя",
                _ => "Произошла ошибка при изменении пользователя"
            };
            return BadRequest(text);
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
            await _usersManager.ChangePasswordAsync(model.Email, model.Password);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InvalidPasswordFormatException => "Неверный формат пароля",
                _ => "Произошла ошибка при изменении пароля"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeBalance(ChangeBalanceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            await _usersManager.ChangeBalanceAsync(model.Id, model.Balance);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка при добавлении подписки"
            };
            return BadRequest(text);
        }
    }

    public async Task<IActionResult> AuthenticateAsUser(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", "Home", new { message = "Пользователь не найден" });
        try
        {
            var user = await _usersManager.GetAuthenticationDataAsync(id.Value);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home", new { message = $"Вы аутентифицированы как {user.UserName}" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка при добавлении подписки"
            };
            return BadRequest(text);
        }
    }
}
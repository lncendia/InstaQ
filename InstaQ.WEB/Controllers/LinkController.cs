using System.Security.Claims;
using InstaQ.WEB.ViewModels.Links;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Links.Exceptions;
using InstaQ.Application.Abstractions.Links.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Links.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class LinkController : Controller
{
    private readonly ILinkManager _linkManager;

    public LinkController(ILinkManager linkManager)
    {
        _linkManager = linkManager;
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLinkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var dto = await _linkManager.AddAsync(id, model.Email);
            return Json(new LinkAddViewModel(User.Identity!.Name!, dto.User2Name, dto.Id));
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                SameUsersException => "Нельзя добавить самого себя",
                TooManyLinksForUserException => "Нельзя добавить больше 20 пользователей",
                LinkAlreadyExistsException => "Пользователь уже добавлен",
                _ => "Произошла ошибка при создании связи"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _linkManager.DeleteAsync(userId, id.Value);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                LinkNotFoundException => "Связь не найдена",
                _ => "Произошла ошибка при удалении связи"
            };
            return BadRequest(text);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Accept(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _linkManager.AcceptAsync(userId, id.Value);
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                LinkNotFoundException => "Связь не найдена",
                LinkAlreadyConfirmedException => "Связь уже подтверждена",
                UserNotFoundException => "Пользователь с таким адресом не найден",
                TooManyLinksForUserException => "Нельзя добавить больше 20 пользователей",
                _ => "Произошла ошибка при создании связи"
            };
            return BadRequest(text);
        }

        return Ok();
    }
}
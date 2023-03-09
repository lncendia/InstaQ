using System.Security.Claims;
using InstaQ.WEB.ViewModels.Participants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Participants.DTOs;
using InstaQ.Application.Abstractions.Participants.Exceptions;
using InstaQ.Application.Abstractions.Participants.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Participants.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class ParticipantsController : Controller
{
    private readonly IParticipantManager _participantManager;
    private readonly IUserParticipantsService _userParticipants;

    public ParticipantsController(IParticipantManager participantManager, IUserParticipantsService userParticipants)
    {
        _participantManager = participantManager;
        _userParticipants = userParticipants;
    }


    [HttpGet]
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Participants(ParticipantsSearchQueryViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var participants = await _participantManager.FindAsync(userId,
                new SearchQuery(model.Page, model.Username, model.Vip, model.HasChild));
            if (!participants.Any()) return BadRequest("Участники не найдены");
            return PartialView("Participants", participants.Select(Map));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }


    private static ParticipantViewModel Map(ParticipantDto participant) =>
        new(participant.Id, participant.Name, participant.Notes, participant.Vip, participant.Children.Select(Map));


    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Участник не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var participant = await _participantManager.GetAsync(userId, id.Value);
            var participants = await _userParticipants.GetUserParticipantsAsync(userId);
            participants.RemoveAll(x => x.id == id);
            ViewBag.Participants = participants;
            return View(new EditParticipantViewModel
            {
                Id = participant.Id,
                Note = participant.Notes,
                ParentId = participant.ParentParticipantId,
                Username = participant.Name,
                Vip = participant.Vip,
            });
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ParticipantNotFoundException => "Участник не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditParticipantViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _participantManager.EditAsync(userId, model.Id, model.ParentId, model.Note, model.Vip);
            return Ok();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ParticipantNotFoundException => "Участник не найден",
                ChildException => "Дочерний элемент не может содержать дочерние элементы",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }
}
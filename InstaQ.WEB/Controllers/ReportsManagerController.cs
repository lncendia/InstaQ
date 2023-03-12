using System.Security.Claims;
using InstaQ.WEB.ViewModels.ReportsManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Links.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsManagement.DTOs;
using InstaQ.Application.Abstractions.ReportsManagement.Exceptions;
using InstaQ.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;
using InstaQ.Domain.ReportLogs.Enums;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.ParticipantReport.Exceptions;
using InstaQ.Domain.Reposts.PublicationReport.Exceptions;
using InstaQ.Domain.Users.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class ReportsManagerController : Controller
{
    private readonly IReportCreationService _reportCreationService;
    private readonly IUserLinksService _userLinksService;

    public ReportsManagerController(IReportCreationService reportCreationService, IUserLinksService userLinksService)
    {
        _reportCreationService = reportCreationService;
        _userLinksService = userLinksService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var links = await _userLinksService.GetUserLinksAsync(userId);
        ViewBag.Links = links;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartParticipantReport(ParticipantReportCreateViewModel model)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _reportCreationService.CreateParticipantReportAsync(userId, model.Timer);
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserTargetException => "Цель не указана",
                UserBalanceException => "Пополните баланс",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }

        return Ok();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartLikeReport(PublicationReportCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _reportCreationService.CreateLikeReportAsync(
                new PublicationReportCreateDto(userId, model.Hashtag, model.AllParticipants, model.CoAuthors),
                model.Timer);
            return Ok();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserBalanceException => "Пополните баланс",
                TooManyLinksException => "Кол-во связей не должно быть больше 3",
                ArgumentException => "Связь не активирована",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartCommentReport(PublicationReportCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _reportCreationService.CreateCommentReportAsync(
                new PublicationReportCreateDto(userId, model.Hashtag, model.AllParticipants, model.CoAuthors),
                model.Timer);
            return Ok();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserBalanceException => "Пополните баланс",
                TooManyLinksException => "Кол-во связей не должно быть больше 3",
                ArgumentException => "Связь не активирована",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CancelReport(CancelReportViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var task = model.ReportType switch
        {
            ReportType.Likes => _reportCreationService.CancelLikeReportAsync(model.Id, userId),
            ReportType.Comments => _reportCreationService.CancelCommentReportAsync(model.Id, userId),
            ReportType.Participants => _reportCreationService.CancelParticipantReportAsync(model.Id, userId),
            _ => throw new ArgumentOutOfRangeException()
        };
        try
        {
            await task;
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчет не найден",
                StoppingJobException => "Не удалось отменить задачу",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }

        return Ok();
    }
}
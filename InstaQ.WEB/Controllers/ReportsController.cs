using System.Security.Claims;
using InstaQ.WEB.ViewModels.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IReportManager _reportManager;
    private readonly Mappers.Abstractions.IReportMapper _reportMapper;

    public ReportsController(IReportManager reportManager, Mappers.Abstractions.IReportMapper reportMapper)
    {
        _reportManager = reportManager;
        _reportMapper = reportMapper;
    }

    [HttpGet]
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Find(ReportsSearchQueryViewModel search)
    {
        if (!ModelState.IsValid) search = new ReportsSearchQueryViewModel();
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var reports = await _reportManager.FindAsync(id,
                new SearchQuery(search.Page, search.ReportType, search.From, search.To));
            return reports.Any() ? PartialView("ReportsList", reports.Select(Map)) : BadRequest();
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

    private static ReportShortViewModel Map(ReportShortDto dto) =>
        new(dto.Id, dto.Hashtag, dto.Type, dto.CreationDate, dto.EndDate, dto.IsCompleted, dto.IsSucceeded, dto.Amount);


    [HttpGet]
    public async Task<IActionResult> LikeReport(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Отчёт не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var report = await _reportManager.GetLikeReportAsync(userId, id.Value);
            ViewBag.Chats = report.LinkedUsers;
            return View(_reportMapper.LikeReportMapper.Value.Map(report));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчет не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> CommentReport(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Отчёт не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var report = await _reportManager.GetCommentReportAsync(userId, id.Value);
            ViewBag.Chats = report.LinkedUsers;
            return View(_reportMapper.CommentReportMapper.Value.Map(report));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчет не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ParticipantsReport(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Отчёт не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var report = await _reportManager.GetParticipantReportAsync(userId, id.Value);
            return View(_reportMapper.ParticipantReportMapper.Value.Map(report));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчет не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }
}
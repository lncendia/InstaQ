using System.Security.Claims;
using InstaQ.WEB.ViewModels.Elements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstaQ.Application.Abstractions.Elements.DTOs;
using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;
using InstaQ.Application.Abstractions.Elements.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;

namespace InstaQ.WEB.Controllers;

[Authorize]
public class ElementsController : Controller
{
    private readonly IReportElementManager _elementManager;
    private readonly Mappers.Abstractions.IElementMapper _elementMapper;

    public ElementsController(IReportElementManager elementManager, Mappers.Abstractions.IElementMapper elementMapper)
    {
        _elementManager = elementManager;
        _elementMapper = elementMapper;
    }

    [HttpGet]
    public async Task<IActionResult> LikeReportElements(PublicationElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetLikeReportElementsAsync(userId, query.ReportId,
                new PublicationElementSearchQuery(query.Page, query.Name, query.Succeeded, query.LikeChatName,
                    query.HasChildren, query.Vip));
            var publicationsViewModels = elements.Publications.Select(Map).ToList();
            var elementsViewModels = elements.Elements.Select(x => _elementMapper.LikeElementMapper.Value.Map(x));
            return elements.Elements.Any()
                ? PartialView("LikeElementsList", new LikeElementsViewModel(elementsViewModels, publicationsViewModels))
                : BadRequest();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчёт не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> CommentReportElements(PublicationElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetCommentReportElementsAsync(userId, query.ReportId,
                new PublicationElementSearchQuery(query.Page, query.Name, query.Succeeded, query.LikeChatName,
                    query.HasChildren, query.Vip));
            var publicationsViewModels = elements.Publications.Select(Map).ToList();
            var elementsViewModels = elements.Elements.Select(x => _elementMapper.CommentElementMapper.Value.Map(x));
            return elements.Elements.Any()
                ? PartialView("CommentElementsList",
                    new CommentElementsViewModel(elementsViewModels, publicationsViewModels))
                : BadRequest();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчёт не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ParticipantReportElements(ParticipantElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetParticipantReportElementsAsync(userId, query.ReportId,
                new ParticipantElementSearchQuery(query.Page, query.Name, query.ElementType, query.HasChildren));
            return elements.Any()
                ? PartialView("ParticipantElementsList",
                    elements.Select(x => _elementMapper.ParticipantElementMapper.Value.Map(x)))
                : BadRequest();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчёт не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }


    private static PublicationViewModel Map(PublicationDto publication) => new(publication.Id, publication.OwnerPk,
        publication.Code, publication.IsLoaded);
}
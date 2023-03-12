using Microsoft.Extensions.Caching.Memory;
using InstaQ.Application.Abstractions.Elements.DTOs;
using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;
using InstaQ.Application.Abstractions.Elements.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Reposts.CommentReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Domain.Reposts.PublicationReport.Entities;

namespace InstaQ.Application.Services.Elements;

public class ElementManager : IReportElementManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElementMapper _elementMapper;
    private readonly IMemoryCache _cache;

    public ElementManager(IUnitOfWork unitOfWork, IElementMapper elementMapper, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _elementMapper = elementMapper;
        _cache = cache;
    }


    public async Task<LikeReportElementsDto> GetLikeReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query)
    {
        if (!_cache.TryGetValue(CachingConstants.GetReportKey(reportId), out LikeReport? report))
        {
            report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
            if (report == null) throw new ReportNotFoundException();
            if (report.IsCompleted)
                _cache.Set(CachingConstants.GetReportKey(reportId), report, TimeSpan.FromMinutes(3));
        }

        if (report!.UserId != userId && !report.LinkedUsers.Contains(userId)) throw new ReportNotFoundException();

        var publications = report.Publications.Where(x => x.IsLoaded.HasValue).Select(Map).ToList();
        var elements = report.Elements.GroupBy(x => x.Parent).ToList();
        var result = new List<LikeReportElement>();
        if (!elements.Any()) return new LikeReportElementsDto(new List<LikeElementDto>(), publications);
        foreach (var element in elements.First(x => x.Key == null).OrderBy(x => x.Name))
        {
            var children = elements.FirstOrDefault(x => x.Key == element)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsPublicationElementValid(element, query);
            var validChildren = children?.Where(x => IsPublicationElementValid(x, query)).OrderBy(x => x.Name).ToList();
            if (valid)
            {
                result.Add(element);
                if (children != null) result.AddRange(children);
            }
            else if (validChildren != null && validChildren.Any())
            {
                result.Add(element);
                result.AddRange(validChildren);
            }
        }

        return new LikeReportElementsDto(
            _elementMapper.LikeReportElementMapper.Value.Map(result.Skip((query.Page - 1) * 50).Take(50).ToList()),
            publications);
    }

    public async Task<CommentReportElementsDto> GetCommentReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query)
    {
        if (!_cache.TryGetValue(CachingConstants.GetReportKey(reportId), out CommentReport? report))
        {
            report = await _unitOfWork.CommentReportRepository.Value.GetAsync(reportId);
            if (report == null) throw new ReportNotFoundException();
            if (report.IsCompleted)
                _cache.Set(CachingConstants.GetReportKey(reportId), report, TimeSpan.FromMinutes(3));
        }

        if (report!.UserId != userId && !report.LinkedUsers.Contains(userId)) throw new ReportNotFoundException();

        var publications = report.Publications.Where(x => x.IsLoaded.HasValue).Select(Map).ToList();
        var elements = report.Elements.GroupBy(x => x.Parent).ToList();
        var result = new List<CommentReportElement>();
        if (!elements.Any()) return new CommentReportElementsDto(new List<CommentElementDto>(), publications);
        foreach (var element in elements.First(x => x.Key == null).OrderBy(x => x.Name))
        {
            var children = elements.FirstOrDefault(x => x.Key == element)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsPublicationElementValid(element, query);
            var validChildren = children?.Where(x => IsPublicationElementValid(x, query)).OrderBy(x => x.Name).ToList();
            if (valid)
            {
                result.Add(element);
                if (children != null) result.AddRange(children);
            }
            else if (validChildren != null && validChildren.Any())
            {
                result.Add(element);
                result.AddRange(validChildren);
            }
        }

        return new CommentReportElementsDto(
            _elementMapper.CommentReportElementMapper.Value.Map(result.Skip((query.Page - 1) * 50).Take(50).ToList()),
            publications);
    }


    private static PublicationDto Map(Publication publication) => new(publication.Id, publication.OwnerPk,
        publication.Code, publication.IsLoaded!.Value);

    private static bool IsPublicationElementValid(PublicationReportElement element, PublicationElementSearchQuery query)
    {
        if (query.Succeeded.HasValue && element.IsAccepted != query.Succeeded.Value)
            return false;

        if (query.Vip.HasValue && element.Vip != query.Vip.Value)
            return false;

        if (!string.IsNullOrEmpty(query.LikeChatName) && !element.LikeChatName.Contains(query.LikeChatName))
            return false;

        if (!string.IsNullOrEmpty(query.NameNormalized))
        {
            var b1 = element.Name.ToUpper().Contains(query.NameNormalized);
            var b2 = element.Pk.Contains(query.NameNormalized);
            if (!(b1 || b2)) return false;
        }

        return true;
    }

    private static bool IsParticipantElementValid(ParticipantReportElement element, ParticipantElementSearchQuery query)
    {
        if (query.ElementType.HasValue && element.Type != query.ElementType.Value)
            return false;

        if (!string.IsNullOrEmpty(query.NameNormalized))
        {
            var b1 = element.Name.ToUpper().Contains(query.NameNormalized);
            var b2 = element.NewName?.ToUpper().Contains(query.NameNormalized) ?? false;
            var b3 = element.Pk.Contains(query.NameNormalized);
            if (!(b1 || b2 || b3)) return false;
        }

        return true;
    }

    public async Task<List<ParticipantElementDto>> GetParticipantReportElementsAsync(Guid userId, Guid reportId,
        ParticipantElementSearchQuery query)
    {
        if (!_cache.TryGetValue(CachingConstants.GetReportKey(reportId), out ParticipantReport? report))
        {
            report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
            if (report == null) throw new ReportNotFoundException();
            if (report.IsCompleted)
                _cache.Set(CachingConstants.GetReportKey(reportId), report, TimeSpan.FromMinutes(3));
        }

        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        var elements = report.Participants.GroupBy(x => x.Parent).ToList();
        var result = new List<ParticipantReportElement>();
        if (!elements.Any()) return new List<ParticipantElementDto>();
        foreach (var element in elements.First(x => x.Key == null).OrderBy(x => x.Name))
        {
            var children = elements.FirstOrDefault(x => x.Key == element)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsParticipantElementValid(element, query);
            var validChildren = children?.Where(x => IsParticipantElementValid(x, query)).OrderBy(x => x.Name).ToList();
            if (valid)
            {
                result.Add(element);
                if (children != null) result.AddRange(children);
            }
            else if (validChildren != null && validChildren.Any())
            {
                result.Add(element);
                result.AddRange(validChildren);
            }
        }

        return _elementMapper.ParticipantReportElementMapper.Value.Map(result.Skip((query.Page - 1) * 100).Take(100));
    }
}
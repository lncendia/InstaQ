using Microsoft.Extensions.Caching.Memory;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.Exceptions;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Ordering;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Ordering;
using InstaQ.Domain.ReportLogs.Ordering.Visitor;
using InstaQ.Domain.ReportLogs.Specification;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Application.Services.ReportsQuery;

public class ReportManager : IReportManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportMapper _mapper;
    private readonly IMemoryCache _cache;

    public ReportManager(IUnitOfWork unitOfWork, IReportMapper mapper, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<List<ReportShortDto>> FindAsync(Guid userId, SearchQuery query)
    {
        ISpecification<ReportLog, IReportLogSpecificationVisitor> specification = new LogByUserIdSpecification(userId);
        if (query.From.HasValue || query.To.HasValue)
            specification = new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(specification,
                new LogByCreationDateSpecification(query.To ?? DateTimeOffset.Now,
                    query.From ?? DateTimeOffset.MinValue));
        if (query.ReportType.HasValue)
            specification = new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(specification,
                new LogByReportTypeSpecification(query.ReportType.Value));
        var reports =
            await _unitOfWork.ReportLogRepository.Value.FindAsync(specification,
                new DescendingOrder<ReportLog, IReportLogSortingVisitor>(new LogByDateOrder()), (query.Page - 1) * 15,
                15);
        return reports.Select(x => new ReportShortDto(x.ReportId, x.AdditionalInfo, x.Type, x.CreatedAt, x.FinishedAt,
            x.IsFinished, x.Success ?? false, x.Amount)).ToList();
    }

    public async Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId && !report.LinkedUsers.Contains(userId)) throw new ReportNotFoundException();
        if (report.IsCompleted) _cache.Set(CachingConstants.GetReportKey(reportId), report, TimeSpan.FromMinutes(3));
        return _mapper.LikeReportMapper.Value.Map(report);
    }

    public async Task<CommentReportDto> GetCommentReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.CommentReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId && !report.LinkedUsers.Contains(userId)) throw new ReportNotFoundException();
        if (report.IsCompleted) _cache.Set(CachingConstants.GetReportKey(reportId), report, TimeSpan.FromMinutes(3));
        return _mapper.CommentReportMapper.Value.Map(report);
    }


    public async Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        if (report.IsCompleted) _cache.Set(CachingConstants.GetReportKey(reportId), report, TimeSpan.FromMinutes(3));

        return _mapper.ParticipantReportMapper.Value.Map(report);
    }
}
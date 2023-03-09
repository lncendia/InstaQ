using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting;
using InstaQ.Infrastructure.DataStorage.Visitors.Specifications;
using Microsoft.EntityFrameworkCore;
using InstaQ.Domain.Abstractions.Repositories;
using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Ordering.Visitor;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Repositories;

internal class LogRepository : IReportLogRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapperUnit<ReportLog, LogModel> _mapper;
    private readonly IModelMapperUnit<LogModel, ReportLog> _modelMapper;
    private readonly LogVisitor _visitor = new();
    private readonly LogSortingVisitor _sortingVisitor = new();


    public LogRepository(ApplicationDbContext context, IAggregateMapperUnit<ReportLog, LogModel> mapper,
        IModelMapperUnit<LogModel, ReportLog> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<ReportLog?> GetAsync(Guid id)
    {
        var model = await _context.Logs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(ReportLog entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Add(model);
    }

    public async Task UpdateAsync(ReportLog entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.Logs.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<ReportLog, IReportLogSpecificationVisitor> specification)
    {
        var query = _context.Logs.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(ISpecification<ReportLog, IReportLogSpecificationVisitor>? specification)
    {
        var query = _context.Logs.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<List<ReportLog>> FindAsync(
        ISpecification<ReportLog, IReportLogSpecificationVisitor>? specification = null,
        IOrderBy<ReportLog, IReportLogSortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        var query = _context.Logs.AsQueryable();
        if (specification != null)
        {
            specification.Accept(_visitor);
            if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        }

        if (orderBy != null)
        {
            orderBy.Accept(_sortingVisitor);
            var firstQuery = _sortingVisitor.SortItems.First();
            var orderedQuery = firstQuery.IsDescending
                ? query.OrderByDescending(firstQuery.Expr)
                : query.OrderBy(firstQuery.Expr);

            query = _sortingVisitor.SortItems.Skip(1)
                .Aggregate(orderedQuery, (current, sort) => sort.IsDescending
                    ? current.ThenByDescending(sort.Expr)
                    : current.ThenBy(sort.Expr));
        }

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        return (await query.AsNoTracking().ToListAsync()).Select(_mapper.Map).ToList();
    }
}
﻿using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;
using InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;
using InstaQ.Infrastructure.DataStorage.Repositories;
using MediatR;
using InstaQ.Domain.Abstractions.Repositories;
using InstaQ.Domain.Abstractions.UnitOfWorks;

namespace InstaQ.Infrastructure.DataStorage;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public UnitOfWork(ApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public Lazy<IUserRepository> UserRepository => new(() =>
        new UserRepository(_context, new UserMapper(), new UserModelMapper(_context)));

    public Lazy<ILinkRepository> LinkRepository => new(() =>
        new LinkRepository(_context, new LinkMapper(), new LinkModelMapper(_context)));

    public Lazy<ITransactionRepository> TransactionRepository => new(() =>
        new TransactionRepository(_context, new TransactionMapper(), new TransactionModelMapper(_context)));

    public Lazy<IParticipantRepository> ParticipantRepository => new(() =>
        new ParticipantRepository(_context, new ParticipantMapper(), new ParticipantModelMapper(_context)));

    public Lazy<IReportLogRepository> ReportLogRepository => new(() =>
        new LogRepository(_context, new LogMapper(), new LogModelMapper(_context)));

    public Lazy<ILikeReportRepository> LikeReportRepository => new(() =>
        new LikeReportRepository(_context, new LikeReportMapper(), new LikeReportModelMapper(_context)));

    public Lazy<ICommentReportRepository> CommentReportRepository => new(() =>
        new CommentReportRepository(_context, new CommentReportMapper(), new CommentReportModelMapper(_context)));

    public Lazy<IParticipantReportRepository> ParticipantReportRepository => new(() =>
        new ParticipantReportRepository(_context, new ParticipantReportMapper(),
            new ParticipantReportModelMapper(_context)));

    public async Task SaveChangesAsync()
    {
        foreach (var notification in _context.Notifications.ToList())
        {
            await _mediator.Publish(notification);
        }

        await _context.SaveChangesAsync();
    }
}
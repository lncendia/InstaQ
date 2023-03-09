using InstaQ.Application.Abstractions.Profile.DTOs;
using InstaQ.Application.Abstractions.Profile.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Links.Specification;
using InstaQ.Domain.Ordering;
using InstaQ.Domain.Participants.Specification;
using InstaQ.Domain.ReportLogs.Entities;
using InstaQ.Domain.ReportLogs.Specification;
using InstaQ.Domain.ReportLogs.Specification.Visitor;
using InstaQ.Domain.Specifications;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Transactions.Entities;
using InstaQ.Domain.Transactions.Ordering;
using InstaQ.Domain.Transactions.Ordering.Visitor;
using InstaQ.Domain.Transactions.Specification;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Specification;
using InstaQ.Domain.Users.Specification.Visitor;

namespace InstaQ.Application.Services.Profile;

public class UserProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<ProfileDto> GetAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        SubscribeDto? subscribe = null;
        TargetDto? target = null;
        if (user.Subscription != null)
        {
            subscribe = new SubscribeDto(user.Subscription.SubscriptionDate, user.Subscription.ExpirationDate);
        }

        if (user.Target != null)
        {
            target = new TargetDto(user.Target.Username, user.Target.ParticipantsType);
        }
        return new ProfileDto(subscribe, target);
    }

    public async Task<StatsDto> GetStatisticAsync(Guid userId)
    {
        var reportsCount = await GetReportsCountAsync(userId);
        var lastMonthReportsCount = await GetLastMonthReportsCountAsync(userId);
        var participantsCount = await GetParticipantsCountAsync(userId);
        return new StatsDto(participantsCount, reportsCount, lastMonthReportsCount);
    }

    private Task<int> GetLastMonthReportsCountAsync(Guid userId)
    {
        return _unitOfWork.ReportLogRepository.Value.CountAsync(
            new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(new LogByUserIdSpecification(userId),
                new LogByCreationDateSpecification(DateTimeOffset.Now, DateTimeOffset.Now.AddMonths(-1))));
    }

    private Task<int> GetReportsCountAsync(Guid userId) =>
        _unitOfWork.ReportLogRepository.Value.CountAsync(new LogByUserIdSpecification(userId));

    private Task<int> GetParticipantsCountAsync(Guid userId) =>
        _unitOfWork.ParticipantRepository.Value.CountAsync(new ParticipantsByUserIdSpecification(userId));


    public async Task<List<LinkDto>> GetLinksAsync(Guid userId)
    {
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdSpecification(userId));
        if (!links.Any()) return new List<LinkDto>();
        var ids = links.SelectMany(l => new[] {l.User1Id, l.User2Id}).Distinct().ToList();
        ISpecification<User, IUserSpecificationVisitor> spec = new UserByIdSpecification(ids.First());
        spec = ids.Skip(1).Aggregate(spec,
            (current, id) =>
                new OrSpecification<User, IUserSpecificationVisitor>(current, new UserByIdSpecification(id)));

        var users = await _unitOfWork.UserRepository.Value.FindAsync(spec);
        return links.Select(l =>
        {
            var user1 = users.FirstOrDefault(x => x.Id == l.User1Id)?.Name ?? throw new UserNotFoundException();
            var user2 = users.FirstOrDefault(x => x.Id == l.User2Id)?.Name ?? throw new UserNotFoundException();
            return new LinkDto(l.Id, user1, user2, l.IsConfirmed, l.User1Id == userId);
        }).ToList();
    }

    public async Task<List<PaymentDto>> GetPaymentsAsync(Guid userId)
    {
        var payments = await _unitOfWork.TransactionRepository.Value.FindAsync(
            new TransactionByUserIdSpecification(userId),
            new DescendingOrder<Transaction, ITransactionSortingVisitor>(new TransactionByCreationDateOrder()));

        return payments.Select(x =>
            new PaymentDto(x.Id, x.PaymentSystemId, x.Amount, x.CreationDate, x.IsSuccessful, x.ConfirmationDate,
                x.PaymentSystemUrl)).ToList();
    }
}
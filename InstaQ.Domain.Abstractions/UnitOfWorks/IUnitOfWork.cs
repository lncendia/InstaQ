using InstaQ.Domain.Abstractions.Repositories;

namespace InstaQ.Domain.Abstractions.UnitOfWorks;

public interface IUnitOfWork
{
    Lazy<IUserRepository> UserRepository { get; }
    Lazy<ILinkRepository> LinkRepository { get; }
    Lazy<IParticipantRepository> ParticipantRepository { get; }
    Lazy<IReportLogRepository> ReportLogRepository { get; }
    Lazy<ILikeReportRepository> LikeReportRepository { get; }
    Lazy<ICommentReportRepository> CommentReportRepository { get; }
    Lazy<IParticipantReportRepository> ParticipantReportRepository { get; }
    Lazy<ITransactionRepository> TransactionRepository { get; }
    Task SaveChangesAsync();
}
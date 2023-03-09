using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;
using InstaQ.Infrastructure.DataStorage.Models.Reports.CommentReport;
using InstaQ.Infrastructure.DataStorage.Models.Reports.LikeReport;
using InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using InstaQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    internal List<INotification> Notifications { get; } = new();

    internal DbSet<UserModel> Users { get; set; } = null!;
    internal DbSet<TransactionModel> Transactions { get; set; } = null!;
    internal DbSet<LogModel> Logs { get; set; } = null!;
    internal DbSet<ParticipantModel> Participants { get; set; } = null!;
    internal DbSet<LinkModel> Links { get; set; } = null!;

    internal DbSet<PublicationModel> Publications { get; set; } = null!;

    internal DbSet<LikeReportModel> LikeReports { get; set; } = null!;
    internal DbSet<LikeReportElementModel> LikeReportElements { get; set; } = null!;

    internal DbSet<CommentReportModel> CommentReports { get; set; } = null!;
    internal DbSet<CommentReportElementModel> CommentReportElements { get; set; } = null!;

    internal DbSet<ParticipantReportModel> ParticipantReports { get; set; } = null!;
    internal DbSet<ParticipantReportElementModel> ParticipantReportElements { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogModel>().HasOne(x => x.User).WithMany();
        modelBuilder.Entity<LinkModel>().HasOne(x => x.User1).WithMany();
        modelBuilder.Entity<LinkModel>().HasOne(x => x.User2).WithMany().OnDelete(DeleteBehavior.ClientCascade);
        modelBuilder.Entity<PublicationReportModel>().HasMany(x => x.LinkedUsers).WithMany();

        modelBuilder.Entity<Model>().UseTpcMappingStrategy();
        modelBuilder.Entity<ElementModel>().UseTpcMappingStrategy();
        base.OnModelCreating(modelBuilder);
    }
}
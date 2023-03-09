using InstaQ.Domain.Reposts.LikeReport.Entities;
using InstaQ.Domain.Reposts.LikeReport.ValueObjects;
using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models.Reports.Base;
using InstaQ.Infrastructure.DataStorage.Models.Reports.LikeReport;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class LikeReportModelMapper : IModelMapperUnit<LikeReportModel, LikeReport>
{
    private readonly ApplicationDbContext _context;

    public LikeReportModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<LikeReportModel> MapAsync(LikeReport model)
    {
        var likeReport = await _context.LikeReports.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (likeReport != null)
        {
            await _context.Entry(likeReport).Collection(x => x.Publications).LoadAsync();
            await _context.Entry(likeReport).Collection(x => x.ElementsList).LoadAsync();
            await _context.Entry(likeReport).Collection(x => x.LinkedUsers).LoadAsync();
        }
        else
        {
            likeReport = new LikeReportModel();
        }


        if (!likeReport.ElementsList.Any())
        {
            likeReport.ElementsList.AddRange(model.Elements.Select(Create));
        }
        else
        {
            var modelElements = model.Elements.OrderBy(x => x.Id).ToList();
            var reportElements = likeReport.ElementsList.OrderBy(x => ((ElementModel) x).Id).ToList();
            for (var i = 0; i < modelElements.Count; i++) Map(modelElements[i], reportElements[i]);
        }

        if (!likeReport.LinkedUsers.Any())
        {
            var users = await _context.Users.Where(x => model.LinkedUsers.Any(y => y == x.Id)).ToListAsync();
            likeReport.LinkedUsers = users;
        }

        ModelInitializer.InitPublicationReportModel(likeReport, model);

        return likeReport;
    }


    private static LikeReportElementModel Create(LikeReportElement element)
    {
        var model = new LikeReportElementModel
        {
            EntityId = element.Id, Name = element.Name, IsAccepted = element.IsAccepted,
            ParticipantId = element.ParticipantId, Pk = element.Pk, LikeChatName = element.LikeChatName,
            Note = element.Note, OwnerId = element.Parent?.Id, Vip = element.Vip,
            Likes = GetLikesRawString(element.Likes)
        };

        return model;
    }

    private static string GetLikesRawString(IEnumerable<LikeInfo> likes) => string.Join(';',
        likes.Select(like => $"{like.PublicationId}:{(like.IsConfirmed ? '1' : '0')}"));

    private static void Map(LikeReportElement element, LikeReportElementModel model)
    {
        model.Likes = GetLikesRawString(element.Likes);
        model.IsAccepted = element.IsAccepted;
    }
}
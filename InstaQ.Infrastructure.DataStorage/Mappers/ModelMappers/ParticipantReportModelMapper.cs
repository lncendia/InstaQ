using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class ParticipantReportModelMapper : IModelMapperUnit<ParticipantReportModel, ParticipantReport>
{
    private readonly ApplicationDbContext _context;

    public ParticipantReportModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<ParticipantReportModel> MapAsync(ParticipantReport model)
    {
        var participantReport = await _context.ParticipantReports.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (participantReport != null)
        {
            await _context.Entry(participantReport).Collection(x => x.ElementsList).LoadAsync();
        }
        else
        {
            participantReport = new ParticipantReportModel();
        }

        if (!participantReport.ElementsList.Any())
        {
            participantReport.ElementsList.AddRange(
                model.Participants.Select(x => Create(x, participantReport)));
        }
        else
        {
            var allElements = model.Participants;
            var deleteElements = participantReport.ElementsList
                .ExceptBy(allElements.Select(x => x.Id), element => element.Id)
                .ToList();
            participantReport.ElementsList.RemoveAll(deleteElements.Contains);

            foreach (var x in model.Participants)
            {
                Map(x, participantReport.ElementsList.First(y => x.Id == y.Id));
            }
        }

        ModelInitializer.InitReportModel(participantReport, model);
        participantReport.Pk = model.Pk;
        participantReport.Type = model.Type;
        return participantReport;
    }


    private static ParticipantReportElementModel Create(ParticipantReportElement element, ParticipantReportModel report)
    {
        var model = new ParticipantReportElementModel
        {
            EntityId = element.Id, Name = element.Name, Type = element.Type, NewName = element.NewName,
            Pk = element.Pk, OwnerId = element.Parent?.Id, Report = report
        };
        return model;
    }

    private static void Map(ParticipantReportElement element, ParticipantReportElementModel model)
    {
        model.Type = element.Type;
        model.NewName = element.NewName;
    }
}
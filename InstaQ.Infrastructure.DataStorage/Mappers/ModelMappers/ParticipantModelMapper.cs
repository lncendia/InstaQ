using InstaQ.Domain.Participants.Entities;
using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class ParticipantModelMapper : IModelMapperUnit<ParticipantModel, Participant>
{
    private readonly ApplicationDbContext _context;

    public ParticipantModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<ParticipantModel> MapAsync(Participant model)
    {
        var participant = await _context.Participants.FirstOrDefaultAsync(x => x.Id == model.Id) ??
                          new ParticipantModel { Id = model.Id };
        participant.Name = model.Name;
        participant.Pk = model.Pk;
        participant.UserId = model.UserId;
        participant.ParentParticipantId = model.ParentParticipantId;
        participant.Notes = model.Notes;
        participant.Vip = model.Vip;
        return participant;
    }
}
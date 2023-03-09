using System.Reflection;
using InstaQ.Domain.Participants.Entities;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ParticipantMapper : IAggregateMapperUnit<Participant, ParticipantModel>
{
    private static readonly FieldInfo OwnerId =
        typeof(Participant).GetField("<ParentParticipantId>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly List<Participant> MockList = new();

    public Participant Map(ParticipantModel model)
    {
        var participant = new Participant(model.UserId, model.Name, model.Pk, MockList);
        IdFields.AggregateId.SetValue(participant, model.Id);
        OwnerId.SetValue(participant, model.ParentParticipantId);
        if (model.Notes != null) participant.SetNotes(model.Notes);
        participant.SetVip(model.Vip);
        return participant;
    }
}
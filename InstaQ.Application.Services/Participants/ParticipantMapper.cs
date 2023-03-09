using InstaQ.Application.Abstractions.Participants.DTOs;
using InstaQ.Application.Abstractions.Participants.ServicesInterfaces;
using InstaQ.Domain.Participants.Entities;

namespace InstaQ.Application.Services.Participants;

public class ParticipantMapper : IParticipantMapper
{
    private static ParticipantDto Map(Participant participant, IEnumerable<ParticipantDto>? children) =>
        new(participant.Id, participant.Name, participant.Notes, participant.Vip, children);

    public List<ParticipantDto> Map(List<Participant> participants)
    {
        var parentParticipants = participants.Where(x => !x.ParentParticipantId.HasValue).ToList();
        var childParticipants = participants.Where(x => x.ParentParticipantId.HasValue)
            .GroupBy(x => x.ParentParticipantId).ToList();
        return parentParticipants.Select(x =>
            Map(x, childParticipants.FirstOrDefault(y => y.Key == x.Id)?.Select(z => Map(z, null)).ToList())).ToList();
    }
}
using InstaQ.Application.Abstractions.Participants.DTOs;
using InstaQ.Domain.Participants.Entities;

namespace InstaQ.Application.Abstractions.Participants.ServicesInterfaces;

public interface IParticipantMapper
{
    List<ParticipantDto> Map(List<Participant> participant);
}
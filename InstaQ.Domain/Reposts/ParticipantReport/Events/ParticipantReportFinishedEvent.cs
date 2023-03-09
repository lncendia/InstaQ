using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Reposts.ParticipantReport.Enums;

namespace InstaQ.Domain.Reposts.ParticipantReport.Events;

public class ParticipantReportFinishedEvent : IDomainEvent
{
    public ParticipantReportFinishedEvent(Guid participantReportId, Guid userId,
        IEnumerable<ParticipantDto> participants)
    {
        ParticipantReportId = participantReportId;
        UserId = userId;
        Participants.AddRange(participants);
    }


    public Guid ParticipantReportId { get; }
    public Guid UserId { get; }
    public List<ParticipantDto> Participants { get; } = new();


    public class ParticipantDto
    {
        public ParticipantDto(Guid? id, string name, string pk, ElementType elementType)
        {
            if (id == null && elementType != ElementType.New)
                throw new ArgumentException("Id must be set for existing participants");
            Id = id;
            Name = name;
            Pk = pk;
            ElementType = elementType;
        }

        public Guid? Id { get; }
        public string Name { get; }
        public string Pk { get; }
        public ElementType ElementType { get; }
    }
}
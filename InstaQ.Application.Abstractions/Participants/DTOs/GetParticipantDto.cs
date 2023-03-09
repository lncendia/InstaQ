namespace InstaQ.Application.Abstractions.Participants.DTOs;

public class GetParticipantDto
{
    public GetParticipantDto(Guid id, Guid? parentParticipantId, string name, string? notes, bool vip)
    {
        Id = id;
        ParentParticipantId = parentParticipantId;
        Name = name;
        Notes = notes;
        Vip = vip;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Notes { get; }
    public bool Vip { get; }

    public Guid? ParentParticipantId { get; }
}
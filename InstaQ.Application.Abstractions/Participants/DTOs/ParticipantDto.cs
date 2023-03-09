namespace InstaQ.Application.Abstractions.Participants.DTOs;

public class ParticipantDto
{
    public ParticipantDto(Guid id, string name, string? notes, bool vip, IEnumerable<ParticipantDto>? children)
    {
        Id = id;
        Name = name;
        Notes = notes;
        Vip = vip;
        Children = children?.ToList() ?? new List<ParticipantDto>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Notes { get; }
    public bool Vip { get; }

    public List<ParticipantDto> Children { get; }
}
namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class ParticipantsResultDto : ResultDto
{
    public ParticipantsResultDto(List<ParticipantDto> participants, int countRequests) : base(countRequests)
    {
        Participants = participants;
    }

    public List<ParticipantDto> Participants { get; }
}

public class ParticipantDto
{
    public ParticipantDto(string pk, string name)
    {
        Pk = pk;
        Name = name;
    }

    public string Pk { get; }
    public string Name { get; }
}
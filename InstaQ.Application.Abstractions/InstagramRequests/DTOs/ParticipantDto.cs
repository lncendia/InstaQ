namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

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
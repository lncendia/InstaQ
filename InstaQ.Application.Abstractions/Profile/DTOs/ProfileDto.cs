using InstaQ.Domain.Users.Enums;

namespace InstaQ.Application.Abstractions.Profile.DTOs;

public class ProfileDto
{
    public ProfileDto(decimal balance, TargetDto? target)
    {
        Balance = balance;
        Target = target;
    }

    public decimal Balance { get; }
    public TargetDto? Target { get; }
}

public class TargetDto
{
    public TargetDto(string name, ParticipantsType participantsType)
    {
        Name = name;
        ParticipantsType = participantsType;
    }

    public string Name { get; }
    public ParticipantsType ParticipantsType { get; }
}
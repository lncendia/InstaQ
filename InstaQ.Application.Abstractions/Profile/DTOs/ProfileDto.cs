using InstaQ.Domain.Users.Enums;

namespace InstaQ.Application.Abstractions.Profile.DTOs;

public class ProfileDto
{
    public ProfileDto(SubscribeDto? subscribe, TargetDto? target)
    {
        Subscribe = subscribe;
        Target = target;
    }

    public SubscribeDto? Subscribe { get; }
    public TargetDto? Target { get; }
}

public class SubscribeDto
{
    public SubscribeDto(DateTimeOffset subscriptionStart, DateTimeOffset subscriptionEnd)
    {
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
    }

    public DateTimeOffset SubscriptionStart { get; }
    public DateTimeOffset SubscriptionEnd { get; }
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
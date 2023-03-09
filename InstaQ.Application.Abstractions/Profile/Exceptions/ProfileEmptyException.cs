using InstaQ.Domain.Users.Enums;

namespace InstaQ.Application.Abstractions.Profile.Exceptions;

public class ProfileEmptyException : Exception
{
    public ParticipantsType Type { get; }

    public ProfileEmptyException(ParticipantsType type) : base("The user has no subscribers/subscriptions")
    {
        Type = type;
    }
}
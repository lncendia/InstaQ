using InstaQ.Domain.Users.Enums;

namespace InstaQ.Domain.Users.ValueObjects;

public class Target
{
    internal Target(string pk, string username, ParticipantsType type)
    {
        Pk = pk;
        ParticipantsType = type;
        Username = username;
        SetDate = DateTimeOffset.Now;
    }

    public string Pk { get; }
    public string Username { get; }
    public DateTimeOffset SetDate { get; }
    public ParticipantsType ParticipantsType { get; }
}
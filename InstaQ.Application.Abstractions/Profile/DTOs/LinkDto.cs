namespace InstaQ.Application.Abstractions.Profile.DTOs;

public class LinkDto
{
    public LinkDto(Guid id, string user1, string user2, bool isConfirmed, bool isSender)
    {
        Id = id;
        User1 = user1;
        User2 = user2;
        IsConfirmed = isConfirmed;
        IsSender = isSender;
    }

    public Guid Id { get;  }
    public string User1 { get; }
    public string User2 { get; }
    public bool IsConfirmed { get; }
    public bool IsSender { get; }
}
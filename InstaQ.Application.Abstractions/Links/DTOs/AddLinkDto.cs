namespace InstaQ.Application.Abstractions.Links.DTOs;

public class AddLinkDto
{
    public AddLinkDto(Guid id, string user2Name)
    {
        Id = id;
        User2Name = user2Name;
    }

    public Guid Id { get; }
    public string User2Name { get; }
}
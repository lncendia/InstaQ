namespace InstaQ.Application.Abstractions.Users.DTOs;

public class UserDto
{
    public UserDto(string username, string email, Guid id, DateTimeOffset? endOfSubscribe)
    {
        Username = username;
        Email = email;
        Id = id;
        EndOfSubscribe = endOfSubscribe;
    }

    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
    public DateTimeOffset? EndOfSubscribe { get; }
}
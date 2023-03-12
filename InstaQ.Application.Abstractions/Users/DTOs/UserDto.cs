namespace InstaQ.Application.Abstractions.Users.DTOs;

public class UserDto
{
    public UserDto(string username, string email, Guid id, decimal balance)
    {
        Username = username;
        Email = email;
        Id = id;
        Balance = balance;
    }

    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
    public decimal Balance { get; }
}
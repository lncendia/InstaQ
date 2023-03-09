namespace InstaQ.Application.Abstractions.Users.Exceptions;

public class UserCreationException : Exception
{
    public UserCreationException(string error) : base($"Failed to create user: {error}")
    {
    }
}
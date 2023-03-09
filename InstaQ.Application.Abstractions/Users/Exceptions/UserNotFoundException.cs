namespace InstaQ.Application.Abstractions.Users.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Can't find user")
    {
    }
}
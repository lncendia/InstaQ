namespace InstaQ.Application.Abstractions.Profile.Exceptions;

public class InstagramNotFoundException : Exception
{
    public InstagramNotFoundException(string username) : base($"Can't find a user from username {username}")
    {
    }
}
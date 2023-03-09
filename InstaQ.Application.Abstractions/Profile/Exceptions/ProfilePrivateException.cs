namespace InstaQ.Application.Abstractions.Profile.Exceptions;

public class ProfilePrivateException : Exception
{
    public ProfilePrivateException(string username) : base($"Profile of {username} is private")
    {
    }
}
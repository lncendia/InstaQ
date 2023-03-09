namespace InstaQ.Application.Abstractions.InstagramRequests.Exceptions;

public class InstagramRequestException : Exception
{
    public int Code { get; }

    public InstagramRequestException(int code, string? message, Exception inner) : base(message, inner)
    {
        Code = code;
    }
}
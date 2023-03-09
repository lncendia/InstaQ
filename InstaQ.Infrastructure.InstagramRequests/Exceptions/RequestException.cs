namespace InstaQ.Infrastructure.InstagramRequests.Exceptions;

public class RequestException : Exception
{
    public int ResponseCode { get; }
    public string? Content { get; }

    public RequestException(int code, string? response)
    {
        ResponseCode = code;
        Content = response;
    }
}
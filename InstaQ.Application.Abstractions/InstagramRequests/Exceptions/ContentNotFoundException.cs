namespace InstaQ.Application.Abstractions.InstagramRequests.Exceptions;

public class ContentNotFoundException : Exception
{
    public ContentNotFoundException() : base("Nothing was found for this query")
    {
    }
}
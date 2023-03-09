namespace InstaQ.Application.Abstractions.Links.Exceptions;

public class LinkNotFoundException : Exception
{
    public LinkNotFoundException() : base("Can't find link")
    {
    }
}
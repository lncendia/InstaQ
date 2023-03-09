namespace InstaQ.Domain.Reposts.PublicationReport.Exceptions;

public class TooManyLinksException : Exception
{
    public TooManyLinksException() : base("Too many links for report. You cannot create more than 3 links")
    {
    }
}
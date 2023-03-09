namespace InstaQ.Domain.Reposts.PublicationReport.Exceptions;

public class PublicationsListEmptyException : Exception
{
    public PublicationsListEmptyException() : base("Publications list is empty")
    {
    }
}
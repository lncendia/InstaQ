namespace InstaQ.Domain.Reposts.PublicationReport.Exceptions;

public class PublicationNotFoundException : Exception
{
    public PublicationNotFoundException():base("Report don't have publication with this id")
    {
    }
}
namespace InstaQ.Domain.Reposts.PublicationReport.Exceptions;

public class LinkNotActiveException : Exception
{
    public Guid Id { get; }
    public LinkNotActiveException(Guid id) : base("One of the links is not activated or does not relate to the user")
    {
        Id = id;
    }
}
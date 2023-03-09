namespace InstaQ.Domain.Reposts.PublicationReport.Exceptions;

public class ElementsListEmptyException : Exception
{
    public ElementsListEmptyException() : base("Elements list is empty")
    {
    }
}
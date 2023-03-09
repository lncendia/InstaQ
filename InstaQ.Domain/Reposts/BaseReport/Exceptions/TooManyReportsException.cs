namespace InstaQ.Domain.Reposts.BaseReport.Exceptions;

public class TooManyReportsException : Exception
{
    public TooManyReportsException() : base("Too many reports. You cannot create more than 25 reports per day")
    {
    }
}
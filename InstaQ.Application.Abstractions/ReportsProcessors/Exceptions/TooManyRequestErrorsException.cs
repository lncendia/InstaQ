namespace InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;

public class TooManyRequestErrorsException : Exception
{
    public TooManyRequestErrorsException(string message) : base(message)
    {
    }
}
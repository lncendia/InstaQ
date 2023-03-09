namespace InstaQ.Application.Abstractions.Payments.Exceptions;

public class ErrorCreateBillException : Exception
{
    public ErrorCreateBillException(string message, Exception? exception) : base(
        $"Error when creating the bill: {message}.", exception)
    {
    }
}
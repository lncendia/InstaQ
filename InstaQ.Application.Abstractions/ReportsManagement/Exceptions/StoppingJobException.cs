namespace InstaQ.Application.Abstractions.ReportsManagement.Exceptions;

public sealed class StoppingJobException : Exception
{
    public StoppingJobException() : base("Failed to stop background job")
    {
    }
}
namespace InstaQ.Domain.Reposts.CommentReport.Exceptions;

public class CommentAlreadyExistException : Exception
{
    public CommentAlreadyExistException() : base("Comment already exist")
    {
    }
}
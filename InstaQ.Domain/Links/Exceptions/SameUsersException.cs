namespace InstaQ.Domain.Links.Exceptions;

public class SameUsersException:Exception
{
    public SameUsersException():base("Users are the same")
    {
    }
}
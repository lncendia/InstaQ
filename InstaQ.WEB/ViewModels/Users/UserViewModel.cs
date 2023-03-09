namespace InstaQ.WEB.ViewModels.Users;

public class UserViewModel
{
    public UserViewModel(Guid id, string username, string email, DateTimeOffset? endOfSubscribe)
    {
        Id = id;
        Username = username;
        Email = email;
        EndOfSubscribe = endOfSubscribe;
    }

    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
    public DateTimeOffset? EndOfSubscribe { get; }
}
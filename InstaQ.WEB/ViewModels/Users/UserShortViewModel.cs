namespace InstaQ.WEB.ViewModels.Users;

public class UserShortViewModel
{
    public UserShortViewModel(Guid id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }

    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
}
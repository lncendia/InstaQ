namespace InstaQ.WEB.ViewModels.Users;

public class UserViewModel
{
    public UserViewModel(Guid id, string username, string email, decimal balance)
    {
        Id = id;
        Username = username;
        Email = email;
        Balance = balance;
    }

    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
    public decimal Balance { get; }
}
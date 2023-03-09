namespace InstaQ.WEB.ViewModels.Profile;


public class LinkViewModel
{
    public LinkViewModel(Guid id, string user1, string user2, bool isConfirmed, bool isSender)
    {
        IsConfirmed = isConfirmed;
        IsSender = isSender;
        Id = id;
        User1 = user1;
        User2 = user2;
    }

    public Guid Id { get; }
    public string User1 { get; }
    public string User2 { get; }
    public bool IsConfirmed { get; }
    public bool IsSender { get; }
}
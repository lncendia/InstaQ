namespace InstaQ.WEB.ViewModels.Links;

public class LinkViewModel
{
    public LinkViewModel(Guid id, string user1, string user2)
    {
        Id = id;
        User1 = user1;
        User2 = user2;
    }

    public Guid Id { get; }
    public string User1 { get; }
    public string User2 { get; }
}
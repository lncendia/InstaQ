namespace InstaQ.WEB.ViewModels.Profile;

public class ProfileViewModel
{
    public ProfileViewModel(string email, string name, StatsViewModel stats, TargetViewModel? target,
        SubscribeViewModel? subscribe, IEnumerable<LinkViewModel> links, IEnumerable<PaymentViewModel> payments)
    {
        Email = email;
        Name = name;
        Stats = stats;
        Target = target;
        Links = links;
        Payments = payments;
        Subscribe = subscribe;
    }

    public string Email { get; }
    public string Name { get; }
    public TargetViewModel? Target { get; }

    public IEnumerable<LinkViewModel> Links { get; }
    public IEnumerable<PaymentViewModel> Payments { get; }
    public StatsViewModel Stats { get; }
    public SubscribeViewModel? Subscribe { get; }
}
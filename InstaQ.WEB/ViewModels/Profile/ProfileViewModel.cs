namespace InstaQ.WEB.ViewModels.Profile;

public class ProfileViewModel
{
    public ProfileViewModel(string email, string name, decimal balance, StatsViewModel stats, TargetViewModel? target,
        IEnumerable<LinkViewModel> links, IEnumerable<PaymentViewModel> payments)
    {
        Email = email;
        Name = name;
        Stats = stats;
        Target = target;
        Links = links;
        Payments = payments;
        Balance = balance;
    }

    public string Email { get; }
    public string Name { get; }
    public decimal Balance { get; }
    public TargetViewModel? Target { get; }

    public IEnumerable<LinkViewModel> Links { get; }
    public IEnumerable<PaymentViewModel> Payments { get; }
    public StatsViewModel Stats { get; }
}
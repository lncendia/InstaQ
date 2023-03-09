namespace InstaQ.WEB.ViewModels.Profile;

public class SubscribeViewModel
{
    public SubscribeViewModel(DateTimeOffset subscriptionStart, DateTimeOffset subscriptionEnd)
    {
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
    }

    public DateTimeOffset SubscriptionStart { get; }
    public DateTimeOffset SubscriptionEnd { get; }
}
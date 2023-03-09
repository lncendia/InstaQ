namespace InstaQ.Domain.Users.ValueObjects;

public class Subscription
{
    internal Subscription(DateTimeOffset expirationDate, DateTimeOffset? startDate = null)
    {
        ExpirationDate = expirationDate;
        SubscriptionDate = startDate ?? DateTimeOffset.Now;
    }

    public DateTimeOffset SubscriptionDate { get; }
    public DateTimeOffset ExpirationDate { get; }

    public bool IsExpired => ExpirationDate < DateTimeOffset.Now;
}
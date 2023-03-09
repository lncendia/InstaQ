using System.Net.Mail;
using System.Text.RegularExpressions;
using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Users.Enums;
using InstaQ.Domain.Users.Exceptions;
using InstaQ.Domain.Users.ValueObjects;

namespace InstaQ.Domain.Users.Entities;

public class User : AggregateRoot
{
    public User(string name, string email)
    {
        Name = name;
        _name = name;
        _email = email;
        Email = email;
    }

    public Subscription? Subscription { get; private set; }
    public Target? Target { get; private set; }

    private string _email;

    /// <exception cref="InvalidEmailException"></exception>
    public string Email
    {
        get => _email;
        set
        {
            try
            {
                _email = new MailAddress(value).Address;
            }
            catch (FormatException)
            {
                throw new InvalidEmailException(value);
            }
        }
    }


    private string _name;

    /// <exception cref="InvalidNicknameException"></exception>
    public string Name
    {
        get => _name;
        set
        {
            if (Regex.IsMatch(value, "^[a-zA-Zа-яА-Я0-9_ ]{3,20}$")) _name = value;
            else throw new InvalidNicknameException(value);
        }
    }

    /// <exception cref="InvalidOperationException"></exception>
    public void AddSubscription(TimeSpan timeSpan)
    {
        if (timeSpan.Ticks <= 0) throw new InvalidOperationException("Time span must be positive");
        var offset = Subscription is {IsExpired: false} ? Subscription.ExpirationDate : DateTimeOffset.Now;
        Subscription = new Subscription(offset.Add(timeSpan), Subscription?.SubscriptionDate);
    }

    public bool IsSubscribed => Subscription is {IsExpired: false};

    public void SetTarget(string pk, string username, ParticipantsType type)
    {
        var dateComparison = DateTimeOffset.Now.AddDays(-1);
        if (Target != null && Target.SetDate > dateComparison)
            throw new TargetChangeException(Target.SetDate - dateComparison);
        Target = new Target(pk, username, type);
    }
}
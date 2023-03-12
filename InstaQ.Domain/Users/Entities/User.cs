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

    public decimal Balance { get; set; }
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

    public void SetTarget(string pk, string username, ParticipantsType type, decimal cost)
    {
        if (Balance < cost) throw new InsufficientFundsException(Id);
        Balance -= cost;
        Target = new Target(pk, username, type);
    }
}
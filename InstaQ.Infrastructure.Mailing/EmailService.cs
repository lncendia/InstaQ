using System.Net;
using System.Net.Mail;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;

namespace InstaQ.Infrastructure.Mailing;

public class EmailService : IEmailService
{
    private readonly SmtpClient _client;
    private readonly string _from;

    public EmailService(string login, string password, string host, int port)
    {
        _client = new SmtpClient
        {
            Host = host,
            Port = port,
            EnableSsl = true,
            Credentials = new NetworkCredential(login, password),
            Timeout = 25000
        };
        _from = login;
    }

    public Task SendEmailAsync(string email, string message)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress(_from, "Soofle.ru");
        mail.To.Add(email);
        mail.Body = message;
        mail.IsBodyHtml = true;
        return _client.SendMailAsync(mail);
    }
}
namespace InstaQ.Application.Abstractions.Users.ServicesInterfaces;

public interface IEmailService
{
    public Task SendEmailAsync(string email, string message);
}
using InstaQ.Application.Abstractions.Users.DTOs;
using InstaQ.Application.Abstractions.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace InstaQ.Application.Abstractions.Users.ServicesInterfaces;

public interface IUserAuthenticationService
{
    Task CreateAsync(UserCreateDto userDto, string confirmUrl);
    Task<UserData> AuthenticateAsync(string username, string password);
    Task ResetPasswordAsync(string email, string code, string newPassword);
    Task RequestResetPasswordAsync(string email, string resetUrl);
    Task<UserData> CodeAuthenticateAsync(string userId, string code);
    Task<UserData> ExternalAuthenticateAsync(ExternalLoginInfo info);
}
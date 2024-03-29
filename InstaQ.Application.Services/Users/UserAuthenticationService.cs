﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using InstaQ.Application.Abstractions.Users.DTOs;
using InstaQ.Application.Abstractions.Users.Entities;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Exceptions;

namespace InstaQ.Application.Services.Users;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<UserData> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public UserAuthenticationService(UserManager<UserData> userManager, IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(UserCreateDto userDto, string confirmUrl)
    {
        var user = new UserData(userDto.Username, userDto.Email);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            Exception ex = result.Errors.First().Code switch
            {
                "MailUsed" => new UserAlreadyExistException(),
                "MailIncorrect" => new InvalidEmailException(userDto.Email),
                "NameIncorrect" => new InvalidNicknameException(userDto.Username),
                _ => new UserCreationException(result.Errors.First().Description)
            };
            throw ex;
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var url = confirmUrl + $"?email={Uri.EscapeDataString(user.Email!)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(user.Email!,
                $"Подтвердите регистрацию, перейдя по <a href=\"{url}\">ссылке</a>.");
        }
        catch (Exception ex)
        {
            await _userManager.DeleteAsync(user);
            throw new EmailException(ex);
        }
    }

    public async Task<UserData> ExternalAuthenticateAsync(ExternalLoginInfo info)
    {
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        if (user != null) return user;
        {
            user = new UserData(
                info.Principal.FindFirstValue(ClaimTypes.GivenName) + ' ' +
                info.Principal.FindFirstValue(ClaimTypes.Surname),
                info.Principal.FindFirstValue(ClaimTypes.Email)!)
            {
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                Exception ex = result.Errors.First().Code switch
                {
                    "MailUsed" => new UserAlreadyExistException(),
                    "MailIncorrect" => new InvalidEmailException(user.Email!),
                    "NameIncorrect" => new InvalidNicknameException(user.UserName!),
                    _ => new UserCreationException(result.Errors.First().Description)
                };
                throw ex;
            }

            await _userManager.AddLoginAsync(user, info);
            var userDomain = new User(user.UserName!, user.Email!);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, userDomain.Id.ToString()));
            await AddAsync(userDomain);
        }

        return user;
    }

    public async Task<UserData> CodeAuthenticateAsync(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded) throw new InvalidCodeException();
        var userDomain = new User(user.UserName!, user.Email!);
        await AddAsync(userDomain);
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, userDomain.Id.ToString()));
        return user;
    }

    public async Task RequestResetPasswordAsync(string email, string resetUrl)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user)) throw new UserNotFoundException();
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        var url = resetUrl + $"?email={Uri.EscapeDataString(email)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(email,
                $"Подтвердите смену пароля, перейдя по <a href = \"{url}\">ссылке</a>.");
        }
        catch (Exception ex)
        {
            await _userManager.DeleteAsync(user);
            throw new EmailException(ex);
        }
    }

    public async Task ResetPasswordAsync(string email, string code, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        var result = await _userManager.ResetPasswordAsync(user, code, newPassword);
        if (!result.Succeeded) throw new InvalidCodeException();
    }

    public async Task<UserData> AuthenticateAsync(string username, string password)
    {
        var user = await _userManager.FindByEmailAsync(username);
        if (user == null) throw new UserNotFoundException();
        var success = await _userManager.CheckPasswordAsync(user, password);
        if (!success) throw new InvalidPasswordException();
        return user;
    }

    private async Task AddAsync(User user)
    {
        await _unitOfWork.UserRepository.Value.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
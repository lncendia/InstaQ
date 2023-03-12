using InstaQ.Application.Abstractions.InstagramRequests.Exceptions;
using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.Profile.Exceptions;
using Microsoft.AspNetCore.Identity;
using InstaQ.Application.Abstractions.Profile.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Entities;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Users.Enums;
using InstaQ.Domain.Users.Exceptions;
using UserNotFoundException = InstaQ.Application.Abstractions.Users.Exceptions.UserNotFoundException;

namespace InstaQ.Application.Services.Profile;

public class SettingsService : ISettingsService
{
    private readonly UserManager<UserData> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInstagramProfileService _pkService;
    private readonly decimal _amount;

    public SettingsService(UserManager<UserData> userManager, IEmailService emailService, IUnitOfWork unitOfWork,
        IInstagramProfileService pkService, decimal amount)
    {
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _pkService = pkService;
        _amount = amount;
    }

    public async Task RequestResetEmailAsync(Guid userId, string newEmail, string resetUrl)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        if (user.Email == newEmail)
            throw new ArgumentException("The new email should be different from the current one.", nameof(newEmail));
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication is null || !userApplication.EmailConfirmed) throw new UserNotFoundException();
        var code = await _userManager.GenerateChangeEmailTokenAsync(userApplication, newEmail);
        var url = resetUrl + $"?email={Uri.EscapeDataString(newEmail)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(user.Email,
                $"Подтвердите смену почты, перейдя по <a href = \"{url}\">ссылке</a>.");
        }
        catch (Exception ex)
        {
            throw new EmailException(ex);
        }
    }

    public async Task ResetEmailAsync(Guid userId, string newEmail, string code)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication is null) throw new UserNotFoundException();
        var result = await _userManager.ChangeEmailAsync(userApplication, newEmail, code);
        if (!result.Succeeded)
        {
            var ex = result.Errors.First().Code switch
            {
                "MailUsed" => new UserAlreadyExistException(),
                "MailIncorrect" => new InvalidEmailException(newEmail),
                "InvalidToken" => new InvalidCodeException(),
                _ => new Exception(result.Errors.First().Description)
            };
            throw ex;
        }

        user.Email = newEmail;
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(string email, string? oldPassword, string newPassword)
    {
        if (oldPassword == newPassword)
        {
            throw new ArgumentException("The new password should be different from the current one.",
                nameof(newPassword));
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        IdentityResult result;
        if (user.PasswordHash == null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
        else
        {
            result = await _userManager.ChangePasswordAsync(user, oldPassword!, newPassword);
        }

        if (!result.Succeeded)
            throw new ArgumentException("The old password is specified incorrectly.", nameof(oldPassword));
    }

    public async Task ChangeNameAsync(Guid userId, string newName)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        user.Name = newName;
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication is null) throw new UserNotFoundException();
        await _userManager.SetUserNameAsync(userApplication, newName);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangeTargetAsync(Guid userId, string target, ParticipantsType type)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        if (user.Balance < _amount) throw new InsufficientFundsException(userId);

        try
        {
            var profile = await _pkService.GetAsync(target);
            if (profile.IsPrivate) throw new ProfilePrivateException(target);
            if (type == ParticipantsType.Followers && profile.FollowersCount == 0)
                throw new ProfileEmptyException(type);
            if (type == ParticipantsType.Followings && profile.FollowingsCount == 0)
                throw new ProfileEmptyException(type);
            user.SetTarget(profile.Pk, target, type, _amount);
            await _unitOfWork.UserRepository.Value.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (ContentNotFoundException)
        {
            throw new UserNotFoundException();
        }
    }
}
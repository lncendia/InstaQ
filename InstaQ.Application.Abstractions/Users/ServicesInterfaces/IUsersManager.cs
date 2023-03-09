using InstaQ.Application.Abstractions.Users.DTOs;
using InstaQ.Application.Abstractions.Users.Entities;

namespace InstaQ.Application.Abstractions.Users.ServicesInterfaces;

public interface IUsersManager
{
    Task<List<UserShortDto>> FindAsync(SearchQuery query);
    Task<UserData> GetAuthenticationDataAsync(Guid userId);
    Task<UserDto> GetAsync(Guid userId);
    Task EditAsync(EditUserDto editData);
    Task ChangePasswordAsync(string email, string password);
    Task AddSubscribeAsync(Guid userId, TimeSpan timeSpan);
}
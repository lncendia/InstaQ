using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface IInstagramProfileService
{
    Task<ProfileDto> GetAsync(string username);
}
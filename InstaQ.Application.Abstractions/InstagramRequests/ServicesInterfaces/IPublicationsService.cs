using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface IPublicationsService
{
    Task<PublicationsResultDto> GetAsync(string hashtag, int count, CancellationToken token);
}
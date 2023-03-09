using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface IPublicationsService
{
    Task<List<PublicationDto>> GetAsync(string hashtag, int count, CancellationToken token);
}
using InstaQ.Application.Abstractions.Profile.DTOs;

namespace InstaQ.Application.Abstractions.Profile.ServicesInterfaces;

public interface IProfileService
{
    Task<ProfileDto> GetAsync(Guid userId);
    Task<StatsDto> GetStatisticAsync(Guid userId);

    Task<List<LinkDto>> GetLinksAsync(Guid userId);

    Task<List<PaymentDto>> GetPaymentsAsync(Guid userId);
}
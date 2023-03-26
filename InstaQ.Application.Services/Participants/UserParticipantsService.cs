using Microsoft.Extensions.Caching.Memory;
using InstaQ.Application.Abstractions.Participants.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Specifications;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Application.Services.Participants;

public class UserParticipantsService : IUserParticipantsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public UserParticipantsService(IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<List<(Guid id, string name)>> GetUserParticipantsAsync(Guid userId)
    {
        if (!_cache.TryGetValue(CachingConstants.GetParticipantsKey(userId), out List<Participant>? participants))
        {
            ISpecification<Participant, IParticipantSpecificationVisitor> participantsSpec =
                new ParticipantsByUserIdSpecification(userId);
            participantsSpec = new AndSpecification<Participant, IParticipantSpecificationVisitor>(participantsSpec,
                new ParentParticipantsSpecification());
            participants = await _unitOfWork.ParticipantRepository.Value.FindAsync(participantsSpec);
        }
        else
        {
            participants = participants!.Where(x => !x.ParentParticipantId.HasValue).ToList();
        }

        return participants.Select(x => (x.Id, x.Name)).ToList();
    }
}
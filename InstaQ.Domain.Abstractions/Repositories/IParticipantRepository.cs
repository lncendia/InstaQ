using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Ordering.Visitor;
using InstaQ.Domain.Participants.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface
    IParticipantRepository : IRepository<Participant, IParticipantSpecificationVisitor, IParticipantSortingVisitor>
{
}
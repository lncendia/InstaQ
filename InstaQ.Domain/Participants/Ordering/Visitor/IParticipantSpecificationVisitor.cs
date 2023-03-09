using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Participants.Entities;

namespace InstaQ.Domain.Participants.Ordering.Visitor;

public interface IParticipantSortingVisitor : ISortingVisitor<IParticipantSortingVisitor, Participant>
{
}
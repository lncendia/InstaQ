using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class ParticipantSortingVisitor : BaseSortingVisitor<ParticipantModel, IParticipantSortingVisitor, Participant>, IParticipantSortingVisitor
{
    protected override List<SortData<ParticipantModel>> ConvertOrderToList(IOrderBy<Participant, IParticipantSortingVisitor> spec)
    {
        var visitor = new ParticipantSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}
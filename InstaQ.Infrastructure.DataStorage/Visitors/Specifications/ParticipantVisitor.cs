using System.Linq.Expressions;
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class ParticipantVisitor : BaseVisitor<ParticipantModel, IParticipantSpecificationVisitor, Participant>,
    IParticipantSpecificationVisitor
{
    protected override Expression<Func<ParticipantModel, bool>> ConvertSpecToExpression(
        ISpecification<Participant, IParticipantSpecificationVisitor> spec)
    {
        var visitor = new ParticipantVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(ParticipantsByUserIdSpecification specification) => Expr = x => x.UserId == specification.UserId;
    public void Visit(ParticipantsByNameSpecification specification) => Expr = x => x.Name.Contains(specification.Name);

    public void Visit(VipParticipantsSpecification specification) => Expr = x => x.Vip;

    public void Visit(ParentParticipantsSpecification specification) => Expr = x => !x.ParentParticipantId.HasValue;

    public void Visit(ChildParticipantsSpecification specification) =>
        Expr = x => x.ParentParticipantId == specification.ParentId;
}
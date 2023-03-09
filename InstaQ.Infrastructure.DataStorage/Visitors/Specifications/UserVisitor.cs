using System.Linq.Expressions;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Specification;
using InstaQ.Domain.Users.Specification.Visitor;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class UserVisitor : BaseVisitor<UserModel, IUserSpecificationVisitor, User>, IUserSpecificationVisitor
{
    protected override Expression<Func<UserModel, bool>> ConvertSpecToExpression(
        ISpecification<User, IUserSpecificationVisitor> spec)
    {
        var visitor = new UserVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(UserByEmailSpecification specification) => Expr = x => x.Email == specification.Email;

    public void Visit(UserByNameSpecification specification) =>
        Expr = x => x.Name.ToUpper().Contains(specification.Name.ToUpper());

    public void Visit(UserByIdSpecification specification) => Expr = x => x.Id == specification.Id;

    public void Visit(UserByIdsSpecification specification) => Expr = x => specification.Ids.Contains(x.Id);
}
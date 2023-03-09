using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Entities;

namespace InstaQ.Domain.Users.Specification.Visitor;

public interface IUserSpecificationVisitor : ISpecificationVisitor<IUserSpecificationVisitor, User>
{
    void Visit(UserByEmailSpecification specification);
    void Visit(UserByNameSpecification specification);
    void Visit(UserByIdSpecification specification);
    void Visit(UserByIdsSpecification specification);
}
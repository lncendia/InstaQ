using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Specification.Visitor;

namespace InstaQ.Domain.Users.Specification;

public class UserByIdSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public Guid Id { get; }
    public UserByIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(User item) => Id == item.Id;

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}
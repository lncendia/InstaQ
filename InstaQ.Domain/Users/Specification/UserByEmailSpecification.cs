using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Specification.Visitor;

namespace InstaQ.Domain.Users.Specification;

public class UserByEmailSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public string Email { get; }
    public UserByEmailSpecification(string email) => Email = email;

    public bool IsSatisfiedBy(User item) => item.Email == Email;

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}
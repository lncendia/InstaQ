using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Specification.Visitor;

namespace InstaQ.Domain.Users.Specification;

public class UserByNameSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public string Name { get; }
    public UserByNameSpecification(string name) => Name = name;

    public bool IsSatisfiedBy(User item) => item.Name.ToUpper().Contains(Name.ToUpper());

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}
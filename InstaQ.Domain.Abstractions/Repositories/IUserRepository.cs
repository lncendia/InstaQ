using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Ordering.Visitor;
using InstaQ.Domain.Users.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface IUserRepository : IRepository<User, IUserSpecificationVisitor, IUserSortingVisitor>
{
}
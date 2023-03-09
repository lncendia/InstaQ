using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Users.Entities;

namespace InstaQ.Domain.Users.Ordering.Visitor;

public interface IUserSortingVisitor : ISortingVisitor<IUserSortingVisitor, User>
{
    void Visit(UserByNameOrder order);
}
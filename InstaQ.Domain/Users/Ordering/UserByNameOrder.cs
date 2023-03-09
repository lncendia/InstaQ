using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Ordering.Visitor;

namespace InstaQ.Domain.Users.Ordering;

public class UserByNameOrder : IOrderBy<User, IUserSortingVisitor>
{
    public IEnumerable<User> Order(IEnumerable<User> items) => items.OrderBy(x => x.Name);

    public IList<IEnumerable<User>> Divide(IEnumerable<User> items) =>
        Order(items).GroupBy(x => x.Name).Select(x => x.AsEnumerable()).ToList();

    public void Accept(IUserSortingVisitor visitor) => visitor.Visit(this);
}
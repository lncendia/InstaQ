using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Ordering;
using InstaQ.Domain.Users.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class UserSortingVisitor : BaseSortingVisitor<UserModel, IUserSortingVisitor, User>, IUserSortingVisitor
{
    protected override List<SortData<UserModel>> ConvertOrderToList(IOrderBy<User, IUserSortingVisitor> spec)
    {
        var visitor = new UserSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(UserByNameOrder order) => SortItems.Add(new SortData<UserModel>(x => x.Name, false));
}
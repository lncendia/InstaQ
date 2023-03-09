using System.Linq.Expressions;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

internal class SortData<TEntity>
{
    public SortData(Expression<Func<TEntity, dynamic>> expr, bool isDescending)
    {
        Expr = expr;
        IsDescending = isDescending;
    }

    public Expression<Func<TEntity, dynamic>> Expr { get; }
    public bool IsDescending { get; }
}
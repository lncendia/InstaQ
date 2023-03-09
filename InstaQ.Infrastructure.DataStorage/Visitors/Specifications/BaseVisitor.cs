using System.Linq.Expressions;
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;
using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Specifications;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal abstract class BaseVisitor<TEntity, TVisitor, TItem> where TVisitor : ISpecificationVisitor<TVisitor, TItem>
    where TEntity : IAggregateModel
    where TItem : AggregateRoot
{
    public Expression<Func<TEntity, bool>>? Expr { get; protected set; }

    protected abstract Expression<Func<TEntity, bool>> ConvertSpecToExpression(ISpecification<TItem, TVisitor> spec);

    public void Visit(AndSpecification<TItem, TVisitor> spec)
    {
        var leftExpr = ConvertSpecToExpression(spec.Left);
        var rightExpr = ConvertSpecToExpression(spec.Right);
        var param = leftExpr.Parameters.Single();
        var exprBody = Expression.AndAlso(leftExpr.Body, Expression.Invoke(rightExpr, param));
        Expr = Expression.Lambda<Func<TEntity, bool>>(exprBody, param);
    }

    public void Visit(OrSpecification<TItem, TVisitor> spec)
    {
        var leftExpr = ConvertSpecToExpression(spec.Left);
        var rightExpr = ConvertSpecToExpression(spec.Right);

        var param = leftExpr.Parameters.Single();
        var exprBody = Expression.Or(leftExpr.Body, Expression.Invoke(rightExpr, param));
        Expr = Expression.Lambda<Func<TEntity, bool>>(exprBody, param);
    }

    public void Visit(NotSpecification<TItem, TVisitor> spec)
    {
        var specExpr = ConvertSpecToExpression(spec.Specification);

        var exprBody = Expression.Not(specExpr.Body);
        Expr = Expression.Lambda<Func<TEntity, bool>>(exprBody, specExpr.Parameters.Single());
    }
}
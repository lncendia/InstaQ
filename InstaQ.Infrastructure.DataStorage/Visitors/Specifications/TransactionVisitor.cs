using System.Linq.Expressions;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Transactions.Entities;
using InstaQ.Domain.Transactions.Specification;
using InstaQ.Domain.Transactions.Specification.Visitor;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class TransactionVisitor : BaseVisitor<TransactionModel, ITransactionSpecificationVisitor, Transaction>,
    ITransactionSpecificationVisitor
{
    protected override Expression<Func<TransactionModel, bool>> ConvertSpecToExpression(
        ISpecification<Transaction, ITransactionSpecificationVisitor> spec)
    {
        var visitor = new TransactionVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(TransactionByUserIdSpecification specification) => Expr = x => x.UserId == specification.Id;
}
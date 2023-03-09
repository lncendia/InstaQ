using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Transactions.Entities;

namespace InstaQ.Domain.Transactions.Ordering.Visitor;

public interface ITransactionSortingVisitor : ISortingVisitor<ITransactionSortingVisitor, Transaction>
{
    void Visit(TransactionByCreationDateOrder sorting);
}
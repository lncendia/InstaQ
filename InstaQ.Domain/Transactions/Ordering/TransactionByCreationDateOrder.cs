using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Transactions.Entities;
using InstaQ.Domain.Transactions.Ordering.Visitor;

namespace InstaQ.Domain.Transactions.Ordering;

public class TransactionByCreationDateOrder : IOrderBy<Transaction, ITransactionSortingVisitor>
{
    public IEnumerable<Transaction> Order(IEnumerable<Transaction> items) => items.OrderBy(x => x.CreationDate);

    public IList<IEnumerable<Transaction>> Divide(IEnumerable<Transaction> items) =>
        Order(items).GroupBy(x => x.CreationDate.Date).Select(x => x.AsEnumerable()).ToList();

    public void Accept(ITransactionSortingVisitor visitor) => visitor.Visit(this);
}
using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Transactions.Entities;
using InstaQ.Domain.Transactions.Ordering;
using InstaQ.Domain.Transactions.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class TransactionSortingVisitor :
    BaseSortingVisitor<TransactionModel, ITransactionSortingVisitor, Transaction>, ITransactionSortingVisitor
{
    protected override List<SortData<TransactionModel>> ConvertOrderToList(
        IOrderBy<Transaction, ITransactionSortingVisitor> spec)
    {
        var visitor = new TransactionSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(TransactionByCreationDateOrder sorting) =>
        SortItems.Add(new SortData<TransactionModel>(x => x.CreationDate, false));
}
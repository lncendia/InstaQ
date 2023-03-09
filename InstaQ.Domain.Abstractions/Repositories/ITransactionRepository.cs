using InstaQ.Domain.Transactions.Entities;
using InstaQ.Domain.Transactions.Ordering.Visitor;
using InstaQ.Domain.Transactions.Specification.Visitor;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface ITransactionRepository : IRepository<Transaction, ITransactionSpecificationVisitor, ITransactionSortingVisitor>
{
}
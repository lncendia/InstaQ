using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Transactions.Entities;
using InstaQ.Domain.Transactions.Specification.Visitor;

namespace InstaQ.Domain.Transactions.Specification;

public class TransactionByUserIdSpecification : ISpecification<Transaction, ITransactionSpecificationVisitor>
{
    public Guid Id { get; }
    public TransactionByUserIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(Transaction item) => item.UserId == Id;

    public void Accept(ITransactionSpecificationVisitor visitor) => visitor.Visit(this);
}
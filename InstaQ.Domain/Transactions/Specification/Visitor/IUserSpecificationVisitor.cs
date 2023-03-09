using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Transactions.Entities;

namespace InstaQ.Domain.Transactions.Specification.Visitor;

public interface ITransactionSpecificationVisitor : ISpecificationVisitor<ITransactionSpecificationVisitor, Transaction>
{
    void Visit(TransactionByUserIdSpecification specification);
}
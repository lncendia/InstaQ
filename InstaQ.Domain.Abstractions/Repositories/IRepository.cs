using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Specifications.Abstractions;

namespace InstaQ.Domain.Abstractions.Repositories;

public interface IRepository<T, out Tx, out Tm> where T : AggregateRoot
    where Tx : ISpecificationVisitor<Tx, T>
    where Tm : ISortingVisitor<Tm, T>
{
    Task<T?> GetAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync(ISpecification<T, Tx>? specification);
    Task<List<T>> FindAsync(ISpecification<T, Tx>? specification = null, IOrderBy<T, Tm>? orderBy = null,
        int? skip = null, int? take = null);
}
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;
using InstaQ.Domain.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IModelMapperUnit<TModel, in TAggregate> where TAggregate : AggregateRoot where TModel : IAggregateModel
{
    Task<TModel> MapAsync(TAggregate model);
}
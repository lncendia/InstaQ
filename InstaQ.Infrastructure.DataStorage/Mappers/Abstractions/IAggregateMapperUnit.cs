using InstaQ.Infrastructure.DataStorage.Models.Abstractions;
using InstaQ.Domain.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IAggregateMapperUnit<out TAggregate, in TModel> where TAggregate : AggregateRoot where TModel : IAggregateModel
{
    TAggregate Map(TModel model);
}
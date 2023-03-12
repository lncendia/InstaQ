using InstaQ.Domain.Users.Entities;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class UserMapper : IAggregateMapperUnit<User, UserModel>
{
    public User Map(UserModel model)
    {
        var user = new User(model.Name, model.Email);
        IdFields.AggregateId.SetValue(user, model.Id);
        if (!string.IsNullOrEmpty(model.TargetPk))
        {
            user.SetTarget(model.TargetPk, model.TargetUsername!, model.ParticipantsType!.Value, decimal.Zero);
        }

        user.Balance = model.Balance;
        return user;
    }
}
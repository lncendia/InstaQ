using System.Reflection;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.ValueObjects;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using InstaQ.Infrastructure.DataStorage.Models;

namespace InstaQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class UserMapper : IAggregateMapperUnit<User, UserModel>
{
    private static readonly FieldInfo UserSubscription =
        typeof(User).GetField("<Subscription>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo TargetDate =
        typeof(Target).GetField("<SetDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public User Map(UserModel model)
    {
        var user = new User(model.Name, model.Email);
        IdFields.AggregateId.SetValue(user, model.Id);
        if (!string.IsNullOrEmpty(model.TargetPk))
        {
            user.SetTarget(model.TargetPk, model.TargetUsername!, model.ParticipantsType!.Value);
            TargetDate.SetValue(user.Target, model.TargetSetTime);
        }
        
        if (model.SubscriptionDate.HasValue)
            UserSubscription.SetValue(user, GetSubscription(model.SubscriptionDate.Value, model.ExpirationDate!.Value));
        return user;
    }

    private static readonly Type SubscriptionElementType = typeof(Subscription);

    private static Subscription GetSubscription(DateTimeOffset start, DateTimeOffset end)
    {
        object?[] args = { end, start };
        return (Subscription)SubscriptionElementType.Assembly.CreateInstance(SubscriptionElementType.FullName!, false,
            BindingFlags.Instance | BindingFlags.NonPublic, null, args!, null, null)!;
    }
}
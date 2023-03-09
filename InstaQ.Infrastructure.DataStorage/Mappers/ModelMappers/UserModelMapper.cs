using InstaQ.Domain.Users.Entities;
using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class UserModelMapper : IModelMapperUnit<UserModel, User>
{
    private readonly ApplicationDbContext _context;

    public UserModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<UserModel> MapAsync(User model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id) ?? new UserModel {Id = model.Id};
        user.Name = model.Name;
        user.Email = model.Email;

        if (model.Subscription != null)
        {
            user.SubscriptionDate = model.Subscription.SubscriptionDate;
            user.ExpirationDate = model.Subscription.ExpirationDate;
        }

        if (model.Target != null)
        {
            user.TargetPk = model.Target.Pk;
            user.TargetUsername = model.Target.Username;
            user.TargetSetTime = model.Target.SetDate;
            user.ParticipantsType = model.Target.ParticipantsType;
        }

        return user;
    }
}
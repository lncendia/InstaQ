using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Infrastructure.InstagramRequests.Abstractions;
using InstaQ.Infrastructure.InstagramRequests.Components;
using InstaQ.Infrastructure.InstagramRequests.Models;
using InstaQ.Infrastructure.InstagramRequests.Services;
using InstaQ.Start.Exceptions;

namespace InstaQ.Start.Extensions;

internal static class InstagramServices
{
    internal static void AddInstagramServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPublicationsService, PublicationsService>();
        services.AddScoped<ILikesService, LikesService>();
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IParticipantsService, ParticipantsService>();
        services.AddScoped<IInstagramProfileService, InstagramProfileService>();
        var token = configuration.GetValue<string>("Lamadava:Token") ??
                    throw new ConfigurationException("Lamadava:Token");
        services.AddScoped<IRequestSender, RequestSender>(_ => new RequestSender(token));
        services.AddScoped<IResponseHandler<(List<CommentModel>, string?)>, CommentsHandler>();
        services.AddScoped<IResponseHandler<(List<LikeModel>, string?)>, LikesHandler>();
        services.AddScoped<IResponseHandler<(List<ParticipantModel>, string?)>, ParticipantsHandler>();
        services.AddScoped<IResponseHandler<(List<PublicationModel>, string?)>, PublicationsHandler>();
        services.AddScoped<IResponseHandler<UserModel>, UserHandler>();
        services.AddScoped<IResponseHandler<ErrorModel>, ErrorHandler>();
    }
}
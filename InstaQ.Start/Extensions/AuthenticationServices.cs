using System.Security.Claims;
using InstaQ.Application.Abstractions.Users.Entities;
using InstaQ.Application.Abstractions.Users.ServicesInterfaces;
using InstaQ.Start.Exceptions;
using Microsoft.AspNetCore.Identity;
using InstaQ.Application.Services.Users;
using InstaQ.Infrastructure.ApplicationDataStorage;

namespace InstaQ.Start.Extensions;

internal static class AuthenticationServices
{
    internal static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var vkOauth = new
        {
            Client = configuration.GetValue<string>("OAuth:Vkontakte:Client") ??
                     throw new ConfigurationException("OAuth:Vkontakte:Client"),
            Secret = configuration.GetValue<string>("OAuth:Vkontakte:Secret") ??
                     throw new ConfigurationException("OAuth:Vkontakte:Secret")
        };

        var yandexOauth = new
        {
            Client = configuration.GetValue<string>("OAuth:Yandex:Client") ??
                     throw new ConfigurationException("OAuth:Yandex:Client"),
            Secret = configuration.GetValue<string>("OAuth:Yandex:Secret") ??
                     throw new ConfigurationException("OAuth:Yandex:Secret")
        };

        services.AddTransient<IUserValidator<UserData>, UserValidator>();

        services.AddIdentity<UserData, RoleData>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.Sid;
            })
            .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

        services.AddAuthentication()
            .AddVkontakte(options =>
            {
                options.ClientId = vkOauth.Client;
                options.ClientSecret = vkOauth.Secret;
                options.Scope.Add("email");
            })
            .AddYandex(options =>
            {
                options.ClientId = yandexOauth.Client;
                options.ClientSecret = yandexOauth.Secret;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Identity.Application", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme);
                policy.RequireClaim(ClaimTypes.Name);
            });
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme);
                policy.RequireClaim(ClaimTypes.Name);
                policy.RequireRole("Admin");
            });
        });
    }
}
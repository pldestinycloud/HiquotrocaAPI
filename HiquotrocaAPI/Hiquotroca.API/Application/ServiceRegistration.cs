using Hiquotroca.API.Application.Interfaces.Services;
using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Hiquotroca.API.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<PasswordHasher<User>>();
        services.AddScoped<ActionTypeService>();
        services.AddScoped <UserService>();
        services.AddScoped<AuthService>();
        services.AddScoped<PostService>();
        services.AddScoped<PromotionalCodeService>();

        return services;
    }
}


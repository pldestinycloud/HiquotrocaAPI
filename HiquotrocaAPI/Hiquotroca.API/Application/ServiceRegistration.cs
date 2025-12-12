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
        //Auth Services
        services.AddScoped<PasswordHasher<User>>();
        services.AddScoped<AuthService>();
        services.AddScoped<TokenService>();

        //Other Entities Services
        services.AddScoped<ActionTypeService>();

        return services;
    }
}


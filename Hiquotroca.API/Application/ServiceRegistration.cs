using FluentValidation;
using Hiquotroca.API.Application.UseCases.Users.Commands.CreateUser;
using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Hiquotroca.API.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Validators
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        //Auth Services
        services.AddScoped<PasswordHasher<User>>();
        services.AddScoped<AuthService>();
        services.AddScoped<TokenService>();

        //Other Entities Services
        services.AddScoped<ActionTypeService>();

        return services;
    }
}


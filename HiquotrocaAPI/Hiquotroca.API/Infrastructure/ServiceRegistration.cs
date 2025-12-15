using Hiquotroca.API.Application.Interfaces;
using Hiquotroca.API.Application.Interfaces.Repositories;
using Hiquotroca.API.Infrastructure.Email;
using Hiquotroca.API.Infrastructure.Jobs;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hiquotroca.API.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool useInMemoryDatabase)
        {
            if (useInMemoryDatabase)
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("HiquotrocaInMemoryDb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")));
            }
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            services.RegisterRepositories();

            return services;
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            // Although the code is running in a single assembly, we use Assembly.GetAssembly to make it more reusable in the future if needed.
            var baseType = typeof(GenericRepository<>);
            var implementations = Assembly.GetAssembly(typeof(ServiceRegistration))!
                .GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.BaseType != null &&
                    t.BaseType.IsGenericType &&
                    t.BaseType.GetGenericTypeDefinition() == baseType);

            foreach (var implementation in implementations)
            {
                services.AddScoped(implementation);
            }
        }
    }
}

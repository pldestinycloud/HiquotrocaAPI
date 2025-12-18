using Hiquotroca.API.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hiquotroca.API.Infrastructure.Persistence.Extensions
{
    public static class AppDbContextExtensions
    {
        public static void ApplyAuditInfo(this DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            var userId = GetUserIdFromContext(httpContextAccessor);
            var entries = context.ChangeTracker.Entries<BaseEntity>();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = utcNow;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = utcNow;
                    entry.Entity.UpdatedBy = userId;
                }
            }
        }

        private static long? GetUserIdFromContext(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
                return null;

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier) ??
                              httpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null)
                return null;

            if (long.TryParse(userIdClaim.Value, out var userId))
                return userId;

            return null;
        }
    }
}
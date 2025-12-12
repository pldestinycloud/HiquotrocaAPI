using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Tabelas principais
        public DbSet<User> Users => Set<User>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<ActionType> ActionTypes => Set<ActionType>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Chat> Chats => Set<Chat>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Subcategory> SubCategories => Set<Subcategory>();
        public DbSet<PromotionalCode> PromotionalCodes => Set<PromotionalCode>();

        public override int SaveChanges()
        {
            this.ApplyAuditInfo(_httpContextAccessor);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo(_httpContextAccessor);
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}



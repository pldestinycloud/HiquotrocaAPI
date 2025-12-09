using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Chat;
using Hiquotroca.API.Domain.Entities.Post;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabelas principais
        public DbSet<User> Users => Set<User>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<ActionType> ActionTypes => Set<ActionType>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Chat> Chats => Set<Chat>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<PromotionalCode> PromotionalCodes => Set<PromotionalCode>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}



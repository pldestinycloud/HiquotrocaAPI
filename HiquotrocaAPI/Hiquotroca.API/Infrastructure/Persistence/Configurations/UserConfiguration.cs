using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.OwnsOne<UserAddress>(u => u.Address);

            builder.HasMany<Post>(u => u.FavoritePosts)
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                       "UserFavoritePost",
                       j => j
                           .HasOne<Post>()
                           .WithMany()
                           .HasForeignKey("PostId")
                           .OnDelete(DeleteBehavior.ClientCascade),
                       j => j
                           .HasOne<User>()
                           .WithMany()
                           .HasForeignKey("UserId")
                           .OnDelete(DeleteBehavior.ClientCascade),
                       j =>
                       {
                           j.HasKey("UserId", "PostId");
                           j.ToTable("UserFavoritePosts");
                       });

            builder.HasMany(u=> u.FollowingUsers)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserFollowing",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.ClientCascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade),
                    j =>
                    {
                        j.HasKey("UserId", "FollowedId");
                        j.ToTable("UserFollowing");
                    });

            builder.HasMany(u => u.PromotionalCodes)
                .WithMany(pc => pc.Owners)
                .UsingEntity<Dictionary<string, object>>(
                    "UserPromotionalCode",
                    j => j
                        .HasOne<PromotionalCode>()
                        .WithMany()
                        .HasForeignKey("PromoCodeId")
                        .OnDelete(DeleteBehavior.ClientCascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade),
                    j =>
                    {
                        j.HasKey("UserId", "PromoCodeId");
                        j.ToTable("UserPromotionalCodes");
                    });
        }
    }
}

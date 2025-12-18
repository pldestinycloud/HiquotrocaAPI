using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Posts.ValueObjects;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.PostTaxonomyData);

            builder.OwnsOne(p => p.Location);

            builder.OwnsOne<PostAdditionalData>(p => p.AdditionalData);

            builder.HasMany<Chat>(p => p.Chats)
                   .WithOne()
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

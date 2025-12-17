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

            builder.OwnsOne(p => p.PostTaxonomyData, taxonomy =>
            {
                taxonomy.HasOne<Category>()
                        .WithMany()
                        .HasForeignKey(t => t.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

                taxonomy.HasOne<Subcategory>()
                        .WithMany()
                        .HasForeignKey(t => t.SubcategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

                taxonomy.HasOne<ActionType>()
                        .WithMany()
                        .HasForeignKey(t => t.ActionTypeId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            builder.OwnsOne(p => p.Location, location =>
            {
                location.HasOne<Country>()
                        .WithMany()
                        .HasForeignKey(l => l.CountryId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

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

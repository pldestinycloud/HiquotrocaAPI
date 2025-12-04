using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany<PostImage>(p => p.Images)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

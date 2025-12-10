using Hiquotroca.API.Domain.Entities;
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
            builder.OwnsOne<PostTaxonomy>(p => p.PostTaxonomyData);
            builder.OwnsOne<PostLocation>(p => p.Location);
            builder.OwnsOne<PostAdditionalData>(p => p.AdditionalData);
        }
    }
}

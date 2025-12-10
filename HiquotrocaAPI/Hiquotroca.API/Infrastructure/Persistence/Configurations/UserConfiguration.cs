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
        }
    }
}

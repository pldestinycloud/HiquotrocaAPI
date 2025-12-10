using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class PromotionalCodeConfiguration : IEntityTypeConfiguration<PromotionalCode>
    {
        public void Configure(EntityTypeBuilder<PromotionalCode> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                   .HasMaxLength(50);
        }
    }
}

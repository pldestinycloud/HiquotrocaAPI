using Hiquotroca.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class PromotionalCodeConfiguration : IEntityTypeConfiguration<PromotionalCode>
    {
        public void Configure(EntityTypeBuilder<PromotionalCode> builder)
        {
            builder.ToTable("PromotionalCodes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.ExpiryDate)
                   .IsRequired();

            builder.HasOne(pc => pc.User)
                   .WithOne(u => u.PromotionalCode)
                   .HasForeignKey<PromotionalCode>(pc => pc.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

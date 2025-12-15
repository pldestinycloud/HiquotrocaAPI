using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations;

public class LotteryConfiguration : IEntityTypeConfiguration<Lottery>
{
    public void Configure(EntityTypeBuilder<Lottery> builder)
    {
        builder.HasKey(l => l.Id);

        builder.OwnsMany<Ticket>(l => l.Tickets, t =>
            {
                t.HasOne<User>().WithMany().HasForeignKey("UserId");
                t.WithOwner().HasForeignKey("LotteryId");
            });
    }
}

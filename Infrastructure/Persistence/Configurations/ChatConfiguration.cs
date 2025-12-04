using Hiquotroca.API.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}

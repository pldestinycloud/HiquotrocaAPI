using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiquotroca.API.Infrastructure.Persistence.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(c => c.UserId1)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(c => c.UserId2)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Post>()
                   .WithMany()
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsMany<Message>(c => c.Messages, m =>
            {
                m.HasKey(m => m.Id);

                m.WithOwner()
                 .HasForeignKey(m => m.ChatId);

                m.HasOne<User>()
                 .WithMany()
                 .HasForeignKey(m => m.SenderId)
                 .OnDelete(DeleteBehavior.Restrict);

                m.HasOne<User>()
                 .WithMany()
                 .HasForeignKey(m => m.ReceiverId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

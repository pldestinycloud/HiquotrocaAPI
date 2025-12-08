using Hiquotroca.API.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class ChatRepository : GenericRepository<Chat>
    {
        private readonly AppDbContext _context;
        public ChatRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Chat>?> GetChatsByUserIdWithFirstMessageAsync(long userId)
        {
            return await _context.Chats
                .Where(c => c.UserId1 == userId || c.UserId2 == userId)
                .Include(c => c.Messages.OrderBy(m => m.CreatedDate).Take(1))
                .ToListAsync();
        }

        public async Task<Chat?> GetChatWithMessagesAsync(long chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }
    }
}
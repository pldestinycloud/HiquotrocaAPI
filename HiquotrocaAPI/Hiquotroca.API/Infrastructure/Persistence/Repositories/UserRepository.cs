using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetUserFavoritePostsAsync(long userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.FavoritePosts)
                .ToListAsync();
        }

        public async Task<List<long>?> GetFollowingUsersForUserAsync(long userId)
        {
            /*return await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.FollowingUsers)
                .ToListAsync();*/

            return new List<long>();
        }

        public async Task<List<User>?> GetUsersByIdsAsync(List<long> usersIds)
        {
            return await _context.Users
                .Where(u => usersIds.Contains(u.Id))
                .ToListAsync();
        }
    }
}

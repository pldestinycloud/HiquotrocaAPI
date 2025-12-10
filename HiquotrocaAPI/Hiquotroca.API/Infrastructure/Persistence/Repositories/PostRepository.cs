using Hiquotroca.API.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>
    {
        private  readonly AppDbContext _context;
        public PostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPostsByIdsAsync(List<long> postsId)
        {
            return await _context.Posts
                .Where(p => postsId.Contains(p.Id))
                .ToListAsync();
        }
    }
}

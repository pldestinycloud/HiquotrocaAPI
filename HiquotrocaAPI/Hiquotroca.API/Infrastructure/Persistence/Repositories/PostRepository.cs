using Hiquotroca.API.Domain.Entities.Post;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }
    }
}

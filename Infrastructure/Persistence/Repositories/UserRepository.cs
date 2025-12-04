using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}

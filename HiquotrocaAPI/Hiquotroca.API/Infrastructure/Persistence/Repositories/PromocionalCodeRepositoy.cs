using Hiquotroca.API.Domain.Entities;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class PromotionalCodeRepository : GenericRepository<PromotionalCode>
    {
        public PromotionalCodeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
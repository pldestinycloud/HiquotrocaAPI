using Hiquotroca.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public class PromotionalCodeRepository : GenericRepository<PromotionalCode>
    {
        private readonly AppDbContext _context;
        public PromotionalCodeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromotionalCode>> GetByIdsAsync(List<long> list)
        {
            return await _context.PromotionalCodes
                .Where(x => list.Contains(x.Id) && !x.IsDeleted)
                .ToListAsync<PromotionalCode>();
        }
    }
}
using Hiquotroca.API.Application.Interfaces.Repositories;
using Hiquotroca.API.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hiquotroca.API.Infrastructure.Persistence.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _context.Set<T>()
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(e => !(e as BaseEntity)!.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is null)
                return; 

            (entity as BaseEntity)!.IsDeleted = true;
            await UpdateAsync(entity);
        }
    }
}

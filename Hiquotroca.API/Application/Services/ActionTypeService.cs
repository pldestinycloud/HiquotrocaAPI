using Hiquotroca.API.Application.Interfaces.Services;
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.DTOs.ActionType;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Services
{
    public class ActionTypeService
    {
        private readonly AppDbContext _db;

        public ActionTypeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ActionTypeDto>> GetAllAsync()
        {
            return await _db.ActionTypes
                .Where(a => !a.IsDeleted)
                .AsNoTracking()
                .Select(a => new ActionTypeDto
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();
        }

        public async Task<ActionTypeDto?> GetByIdAsync(long id)
        {
            return await _db.ActionTypes
                .Where(a => a.Id == id && !a.IsDeleted)
                .AsNoTracking()
                .Select(a => new ActionTypeDto
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ActionTypeDto> CreateAsync(ActionTypeCreateRequest request, long userId)
        {
            var actionType = new ActionType(
                name: request.Name,
                description: request.Description
                );

            _db.ActionTypes.Add(actionType);
            await _db.SaveChangesAsync();

            return new ActionTypeDto
            {
                Id = actionType.Id,
                Name = actionType.Name
            };
        }

        public async Task<bool> UpdateAsync(long id, ActionTypeUpdateRequest request, long userId)
        {
            var entity = await _db.ActionTypes.FindAsync(id);
            if (entity == null || entity.IsDeleted) return false;

            /*entity.Name = request.Name;
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.UtcNow;*/

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id, long userId)
        {
            var entity = await _db.ActionTypes.FindAsync(id);

            if (entity == null || entity.IsDeleted)            
                return false;

            entity.IsDeleted = true;
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }
    }


}

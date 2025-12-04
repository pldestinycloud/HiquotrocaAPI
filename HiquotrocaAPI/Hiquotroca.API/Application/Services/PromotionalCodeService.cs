using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Services
{
    public class PromotionalCodeService
    {
        private readonly AppDbContext _context;

        public PromotionalCodeService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<BaseResult<long>> CreateAsync(CreatePromotionalCodeDto dto, long userId)
        {
            var code = new PromotionalCode
            {
                Code = dto.Code,
                ExpiryDate = dto.ExpiryDate,
                IsActive = true,
                UserId = dto.UserId,

                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            _context.PromotionalCodes.Add(code);
            await _context.SaveChangesAsync();

            return BaseResult<long>.Ok(code.Id);
        }
        public async Task<BaseResult<PromotionalCode?>> GetByIdAsync(long id)
        {
            return await _context.PromotionalCodes.FindAsync(id);
        }
        public async Task<BaseResult<List<PromotionalCode>>> GetAllAsync()
        {
            return await _context.PromotionalCodes.ToListAsync();
        }
        public async Task<BaseResult<PromotionalCode>> UpdateAsync(UpdatePromotionalCodeDto dto, long currentUserId)
        {
            var code = await _context.PromotionalCodes.FindAsync(dto.Id);

            if (code == null)
                throw new Exception("Promotional Code not found");

            code.Code = dto.Code;
            code.ExpiryDate = dto.ExpiryDate;
            code.IsActive = dto.IsActive;

            code.UpdatedBy = currentUserId;
            code.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return code;
        }
        public async Task<BaseResult<bool>> DeleteAsync(long id, long currentUserId)
        {
            var code = await _context.PromotionalCodes
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (code == null)
                return BaseResult<bool>.Failure(new Error(ErrorCode.NotFound, "Promotional code not found"));

            code.IsDeleted = true;
            code.UpdatedBy = currentUserId;
            code.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return BaseResult<bool>.Ok(true);
        }
    }
}

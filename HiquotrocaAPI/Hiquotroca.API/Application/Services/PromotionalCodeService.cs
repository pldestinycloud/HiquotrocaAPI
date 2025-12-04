using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence;
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


        public async Task<BaseResult<long>> CreateAsync(CreatePromotionalCodeDto dto)
        {
            var code = new PromotionalCode
            {
                Code = dto.Code,
                ExpiryDate = dto.ExpiryDate,
                IsActive = true,
                UserId = dto.UserId
            };

            _context.PromotionalCodes.Add(code);
            await _context.SaveChangesAsync();

            return code.Id;
        }
        public async Task<PromotionalCode?> GetByIdAsync(long id)
        {
            return await _context.PromotionalCodes.FindAsync(id);
        }
        public async Task<List<PromotionalCode>> GetAllAsync()
        {
            return await _context.PromotionalCodes.ToListAsync();
        }
        public async Task<PromotionalCode> UpdateAsync(UpdatePromotionalCodeDto dto)
        {
            var code = await _context.PromotionalCodes.FindAsync(dto.Id);

            if (code == null)
                throw new Exception("Promotional Code not found");

            code.Code = dto.Code;
            code.ExpiryDate = dto.ExpiryDate;
            code.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return code;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var code = await _context.PromotionalCodes.FindAsync(id);
            if (code == null)
                return false;

            _context.PromotionalCodes.Remove(code);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

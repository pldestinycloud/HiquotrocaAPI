using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence.Repositories; // Importar Repositórios

namespace Hiquotroca.API.Application.Services
{
    public class PromotionalCodeService
    {
        // Alterado de AppDbContext para o Repositório específico
        private readonly PromotionalCodeRepository _repository;

        public PromotionalCodeService(PromotionalCodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResult<long>> CreateAsync(CreatePromotionalCodeDto dto, long userId)
        {
            var code = new PromotionalCode(dto.Code,dto.ExpiryDate);
            _context.PromotionalCodes.Add(code);
            await _context.SaveChangesAsync();

            return BaseResult<long>.Ok(code.Id);
        }

        public async Task<BaseResult<PromotionalCode?>> GetByIdAsync(long id)
        {
            var code = await _repository.GetByIdAsync(id);
            return BaseResult<PromotionalCode?>.Ok(code);
        }

        public async Task<BaseResult<IEnumerable<PromotionalCode>>> GetAllAsync()
        {
            var codes = await _repository.GetAllAsync();
            return BaseResult<IEnumerable<PromotionalCode>>.Ok(codes);
        }

        public async Task<BaseResult<PromotionalCode>> UpdateAsync(UpdatePromotionalCodeDto dto, long currentUserId)
        {
            var code = await _repository.GetByIdAsync(dto.Id);

            if (code == null)
                return BaseResult<PromotionalCode>.Failure(new Error(ErrorCode.NotFound, "Promotional Code not found"));

            code.UpdatedBy = currentUserId;
            code.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(code);

            return BaseResult<PromotionalCode>.Ok(code);
        }

        public async Task<BaseResult<bool>> DeleteAsync(long id, long currentUserId)
        {
            var code = await _repository.GetByIdAsync(id);

            if (code == null)
                return BaseResult<bool>.Failure(new Error(ErrorCode.NotFound, "Promotional code not found"));

            // O DeleteAsync do GenericRepository já trata o Soft Delete (IsDeleted = true)
            code.UpdatedBy = currentUserId; // Opcional: registar quem apagou antes de apagar

            await _repository.DeleteAsync(code);

            return BaseResult<bool>.Ok(true);
        }

        public async Task<List<PromotionalCode>> GetPromotionalCodesByIdsAsync(List<long> list)
        {
            return await _context.PromotionalCodes
                .Where(x => list.Contains(x.Id) && !x.IsDeleted)
                .ToListAsync();
        }
    }
}
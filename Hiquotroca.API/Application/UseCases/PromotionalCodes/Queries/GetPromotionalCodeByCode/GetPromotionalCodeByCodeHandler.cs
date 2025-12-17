using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.PromotionalCodes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Queries.GetPromotionalCodeById;

public class GetPromotionalCodeByCodeHandler(AppDbContext db) : IRequestHandler<GetPromotionalCodeByCodeQuery, PromotionalCodeDto?>
{
    public async Task<PromotionalCodeDto?> Handle(GetPromotionalCodeByCodeQuery request, CancellationToken cancellationToken)
    {
        var promoCode = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Code == request.Code);
        if (promoCode == null)
            return null;

        return MapPromotionalCodeToPromotionalCodeDto.Map(promoCode, new PromotionalCodeDto());
    }
}

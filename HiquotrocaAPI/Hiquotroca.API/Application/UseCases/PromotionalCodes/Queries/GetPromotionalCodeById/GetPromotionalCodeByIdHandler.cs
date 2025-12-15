using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.PromotionalCodes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodeById;

public class GetPromotionalCodeByIdHandler(AppDbContext db) : IRequestHandler<GetPromotionalCodeByIdQuery, PromotionalCodeDto?>
{
    public async Task<PromotionalCodeDto?> Handle(GetPromotionalCodeByIdQuery request, CancellationToken cancellationToken)
    {
        var promoCode = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (promoCode == null)
            return null;

        return MapPromotionalCodeToPromotionalCodeDto.Map(promoCode, new PromotionalCodeDto());
    }
}

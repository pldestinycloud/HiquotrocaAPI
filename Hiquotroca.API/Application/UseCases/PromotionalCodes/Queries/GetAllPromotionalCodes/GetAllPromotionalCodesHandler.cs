using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.PromotionalCodes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Queries.GetAllPromotionalCodes;

public class GetAllPromotionalCodesHandler(AppDbContext db) : IRequestHandler<GetAllPromotionalCodesQuery, List<PromotionalCodeDto>>
{
    public async Task<List<PromotionalCodeDto>> Handle(GetAllPromotionalCodesQuery request, CancellationToken cancellationToken)
    {
        var promoCodes = await db.PromotionalCodes.ToListAsync();
        if(promoCodes == null || !promoCodes.Any())
            return new List<PromotionalCodeDto>();


        return promoCodes.Select(pc => MapPromotionalCodeToPromotionalCodeDto.Map(pc, new PromotionalCodeDto())).ToList();
    }
}

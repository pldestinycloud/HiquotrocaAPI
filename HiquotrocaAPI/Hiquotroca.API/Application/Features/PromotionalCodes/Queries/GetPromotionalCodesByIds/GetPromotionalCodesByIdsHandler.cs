using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodesByIds;

public class GetPromotionalCodesByIdsHandler(AppDbContext db) : IRequestHandler<GetPromotionalCodesByIdsQuery, List<Domain.Entities.PromotionalCode>>
{
    public async Task<List<Domain.Entities.PromotionalCode>> Handle(GetPromotionalCodesByIdsQuery request, CancellationToken cancellationToken)
    {
        return await db.PromotionalCodes.Where(p => request.PromoCodesIds.Contains(p.Id)).ToListAsync();
    }
}

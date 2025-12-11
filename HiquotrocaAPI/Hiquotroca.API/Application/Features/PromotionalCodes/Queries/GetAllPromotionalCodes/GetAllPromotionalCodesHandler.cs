using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetAllPromotionalCodes;

public class GetAllPromotionalCodesHandler(AppDbContext db) : IRequestHandler<GetAllPromotionalCodesQuery, List<Domain.Entities.PromotionalCode>>
{
    public async Task<List<Domain.Entities.PromotionalCode>> Handle(GetAllPromotionalCodesQuery request, CancellationToken cancellationToken)
    {
        return await db.PromotionalCodes.ToListAsync();
    }
}

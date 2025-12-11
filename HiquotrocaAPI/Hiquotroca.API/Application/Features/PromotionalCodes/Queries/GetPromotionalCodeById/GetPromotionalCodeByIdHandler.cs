using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Queries.GetPromotionalCodeById;

public class GetPromotionalCodeByIdHandler(AppDbContext db) : IRequestHandler<GetPromotionalCodeByIdQuery, Domain.Entities.PromotionalCode?>
{
    public async Task<Domain.Entities.PromotionalCode?> Handle(GetPromotionalCodeByIdQuery request, CancellationToken cancellationToken)
    {
        return await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.Id);
    }
}

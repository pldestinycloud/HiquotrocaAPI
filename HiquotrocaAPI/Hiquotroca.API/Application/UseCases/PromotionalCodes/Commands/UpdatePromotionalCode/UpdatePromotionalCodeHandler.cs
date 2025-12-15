using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.UpdatePromotionalCode;

public class UpdatePromotionalCodeHandler(AppDbContext db) : IRequestHandler<UpdatePromotionalCodeCommand>
{
    public async Task Handle(UpdatePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var code = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.promotionalCodeId);
        if (code == null)
           throw new KeyNotFoundException("Promotional code not found.");

        code.Update(request.code, request.expiryDate, request.isActive);

        db.PromotionalCodes.Update(code);
        await db.SaveChangesAsync();
    }
}
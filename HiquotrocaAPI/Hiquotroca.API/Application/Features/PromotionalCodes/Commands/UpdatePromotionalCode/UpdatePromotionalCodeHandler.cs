using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.UpdatePromotionalCode;

public class UpdatePromotionalCodeHandler(AppDbContext db) : IRequestHandler<UpdatePromotionalCodeCommand, Domain.Entities.PromotionalCode?>
{
    public async Task<Domain.Entities.PromotionalCode?> Handle(UpdatePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var code = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.Dto.Id);

        if (code == null)
            return null;

        db.PromotionalCodes.Update(code);
        await db.SaveChangesAsync();

        return code;
    }
}
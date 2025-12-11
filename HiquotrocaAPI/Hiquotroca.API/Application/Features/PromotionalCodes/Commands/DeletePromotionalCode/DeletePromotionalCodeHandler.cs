using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.DeletePromotionalCode;

public class DeletePromotionalCodeHandler(AppDbContext db) : IRequestHandler<DeletePromotionalCodeCommand, bool>
{
    public async Task<bool> Handle(DeletePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var code = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (code == null)
            return false;
        code.UpdatedBy = request.CurrentUserId;
        db.PromotionalCodes.Remove(code);
        await db.SaveChangesAsync();
        return true;
    }
}

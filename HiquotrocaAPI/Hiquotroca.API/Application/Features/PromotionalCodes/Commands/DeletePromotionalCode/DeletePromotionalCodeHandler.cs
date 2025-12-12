using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.DeletePromotionalCode;

public class DeletePromotionalCodeHandler(AppDbContext db) : IRequestHandler<DeletePromotionalCodeCommand>
{
    public async Task Handle(DeletePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var code = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.Id);

        if (code == null)
            throw new KeyNotFoundException($"Promotional code with ID {request.Id} not found.");

        db.PromotionalCodes.Remove(code);
        await db.SaveChangesAsync();
    }
}

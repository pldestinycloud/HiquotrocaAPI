using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.AttributePromotionalCode;

public class AttributePromotionalCodeHandler(AppDbContext db) : IRequestHandler<AttributePromotionalCodeCommand>
{
    public async Task Handle(AttributePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return;

        //user.AddPromotionalCode(request.PromoCodeId);
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Commands.CreatePromotionalCode;

public class CreatePromotionalCodeHandler(AppDbContext db) : IRequestHandler<CreatePromotionalCodeCommand>
{
    public async Task Handle(CreatePromotionalCodeCommand command, CancellationToken cancellationToken)
    {
        if( await db.PromotionalCodes.AnyAsync(pc => pc.Code == command.Code, cancellationToken))
            throw new InvalidOperationException("Promotional code already exists.");
       
        var code = new PromotionalCode(command.Code, command.ExpiryDate, command.BonusPercentage);
        await db.PromotionalCodes.AddAsync(code);
        await db.SaveChangesAsync();
    }
}

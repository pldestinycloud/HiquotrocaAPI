using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.CreatePromotionalCode;

public class CreatePromotionalCodeHandler(AppDbContext db) : IRequestHandler<CreatePromotionalCodeCommand>
{
    public async Task Handle(CreatePromotionalCodeCommand command, CancellationToken cancellationToken)
    {
        var code = new PromotionalCode(command.Code, command.ExpiryDate);
        await db.PromotionalCodes.AddAsync(code);
        await db.SaveChangesAsync();
    }
}

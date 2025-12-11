using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.CreatePromotionalCode;

public class CreatePromotionalCodeHandler(AppDbContext db) : IRequestHandler<CreatePromotionalCodeCommand, long>
{
    public async Task<long> Handle(CreatePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var code = new PromotionalCode(request.Dto.Code, request.Dto.ExpiryDate);
        await db.PromotionalCodes.AddAsync(code);
        await db.SaveChangesAsync();
        return code.Id;
    }
}

using Hiquotroca.API.DTOs.PromotionalCode;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.PromotionalCodes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.UseCases.PromotionalCodes.Queries.GetPromotionalCodesOfUser;

public class GetPromotionalCodesOfUserHandler(AppDbContext dbContext) : IRequestHandler<GetPromotionalCodesOfUserQuery, List<PromotionalCodeDto>>
{
    public async Task<List<PromotionalCodeDto>> Handle(GetPromotionalCodesOfUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync(request.UserId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

        var promoCodes = await dbContext.PromotionalCodes
            .Where(pc => pc.Owners.Contains(user))
            .ToListAsync();

        return promoCodes.Select(pc => MapPromotionalCodeToPromotionalCodeDto.Map(pc, new PromotionalCodeDto())).ToList();
    }
}

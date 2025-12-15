using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.DTOs.Lotteries;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Lotteries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.Lotteries.Queries.GetLotteryById;

public class GetLotteryByIdHandler(AppDbContext db) : IRequestHandler<GetLotteryByIdQuery, LotteryDto?>
{
    public async Task<LotteryDto?> Handle(GetLotteryByIdQuery request, CancellationToken cancellationToken)
    {
        var lottery = await db.Set<Lottery>()
            .Include(l => l.Tickets)
            .FirstOrDefaultAsync(l => l.Id == request.Id && !l.IsDeleted, cancellationToken);

        if (lottery == null) return null;

        return MapLotteryToLotteryDto.Map(lottery);
    }
}
using Hiquotroca.API.DTOs.Lotteries;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Lotteries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.Lotteries.Queries.GetAllLotteries;

public class GetAllLotteriesHandler(AppDbContext db) : IRequestHandler<GetAllLotteriesQuery, List<LotteryDto>>
{
    public async Task<List<LotteryDto>> Handle(GetAllLotteriesQuery request, CancellationToken cancellationToken)
    {
        var lotteries = await db.Set<Hiquotroca.API.Domain.Entities.Lottery>()
            .Include(l => l.Tickets) 
            .Where(l => !l.IsDeleted)
            .OrderByDescending(l => l.CreatedDate)
            .ToListAsync(cancellationToken);

        return lotteries.Select(MapLotteryToLotteryDto.Map).ToList();
    }
}
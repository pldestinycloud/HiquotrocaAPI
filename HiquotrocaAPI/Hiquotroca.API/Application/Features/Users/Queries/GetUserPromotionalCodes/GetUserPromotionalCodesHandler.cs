using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserPromotionalCodes;

public class GetUserPromotionalCodesHandler(AppDbContext db) : IRequestHandler<GetUserPromotionalCodesQuery, List<long>>
{
    public async Task<List<long>> Handle(GetUserPromotionalCodesQuery request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return new List<long>();

        //return user.PromotionalCodes.ToList();
        return new List<long>();
    }
}
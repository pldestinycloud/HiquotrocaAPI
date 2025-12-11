using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.UnfollowUser;

public class UnfollowUserHandler(AppDbContext db) : IRequestHandler<UnfollowUserCommand>
{
    public async Task Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return;

        //user.StopFollowing(request.TargetUserId);
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}

using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.FollowUser;

public class FollowUserHandler(AppDbContext db) : IRequestHandler<FollowUserCommand>
{
    public async Task Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return;

        //user.StartFollowing(request.TargetUserId);
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}

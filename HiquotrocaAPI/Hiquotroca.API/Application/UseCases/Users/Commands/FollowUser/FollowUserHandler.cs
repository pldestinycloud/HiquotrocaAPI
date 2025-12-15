using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.FollowUser;

public class FollowUserHandler(AppDbContext db) : IRequestHandler<FollowUserCommand>
{
    public async Task Handle(FollowUserCommand command, CancellationToken cancellationToken)
    {
        var user = await db.Users
            .Include(u => u.FollowingUsers)
            .FirstOrDefaultAsync(u => u.Id == command.UserId);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        var targetUser = await db.Users.FirstOrDefaultAsync(u => u.Id == command.TargetUserId);
        if (targetUser == null)
            throw new KeyNotFoundException("Target user not found.");

        user.StartFollowing(targetUser);

        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}

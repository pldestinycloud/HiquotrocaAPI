using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.UnfollowUser;

public class UnfollowUserHandler(AppDbContext db) : IRequestHandler<UnfollowUserCommand>
{
    public async Task Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users
            .Include(u => u.FollowingUsers)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        var targetUser = await db.Users.FirstOrDefaultAsync(u => u.Id == request.TargetUserId);
        if (targetUser == null)
            throw new KeyNotFoundException("Target user not found.");

        user.StopFollowing(targetUser);
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}

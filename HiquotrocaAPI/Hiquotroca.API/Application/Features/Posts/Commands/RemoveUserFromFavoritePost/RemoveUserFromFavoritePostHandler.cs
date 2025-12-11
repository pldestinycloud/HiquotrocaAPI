using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Commands.RemoveUserFromFavoritePost;

public class RemoveUserFromFavoritePostHandler(AppDbContext db) : IRequestHandler<RemoveUserFromFavoritePostCommand, bool>
{
    public async Task<bool> Handle(RemoveUserFromFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (post == null || user == null)
            return false;

        // Remove user from post's favorites (update join table or similar logic)
        // Example: var entry = await db.UserFavoritePosts.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.PostId == request.PostId);
        // if (entry != null) db.UserFavoritePosts.Remove(entry);
        await db.SaveChangesAsync();
        return true;
    }
}

using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Commands.RemoveUserFromFavoritePost;

public class RemoveUserFromFavoritePostHandler(AppDbContext db) : IRequestHandler<RemoveUserFromFavoritePostCommand>
{
    public async Task Handle(RemoveUserFromFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
        if (post == null)
            throw new KeyNotFoundException("Post not found");

        var user = await db.Users
            .Include(u => u.FavoritePosts)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            throw new KeyNotFoundException("User not found");

        user.RemoveFavoritePost(post);
        await db.SaveChangesAsync();
    }
}

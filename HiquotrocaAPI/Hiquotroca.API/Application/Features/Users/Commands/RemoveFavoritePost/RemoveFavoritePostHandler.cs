using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.RemoveFavoritePost;

public class RemoveFavoritePostHandler(AppDbContext db) : IRequestHandler<RemoveFavoritePostCommand>
{
    public async Task Handle(RemoveFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.Include(u => u.FavoritePosts).FirstOrDefaultAsync(u => u.Id == request.UserId);
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
        if (user == null || post == null)
            return;

        user.RemoveFavoritePost(post);
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}

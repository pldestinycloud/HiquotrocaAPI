using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Commands.AddUserToFavoritePost;

public class AddUserToFavoritePostHandler(AppDbContext db) : IRequestHandler<AddUserToFavoritePostCommand, bool>
{
    public async Task<bool> Handle(AddUserToFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (post == null || user == null)
            return false;

        // Add user to post's favorites (update join table or similar logic)
        // Example: db.UserFavoritePosts.Add(new UserFavoritePost { UserId = request.UserId, PostId = request.PostId });
        await db.SaveChangesAsync();
        return true;
    }
}

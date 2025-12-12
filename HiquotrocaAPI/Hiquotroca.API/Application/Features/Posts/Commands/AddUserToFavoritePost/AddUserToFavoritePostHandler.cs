using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Commands.AddUserToFavoritePost;

public class AddUserToFavoritePostHandler(AppDbContext db) : IRequestHandler<AddUserToFavoritePostCommand>
{
    public async Task Handle(AddUserToFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
        if(post == null)
            throw new KeyNotFoundException("Post not found");

        var user = await db.Users
            .Include(u => u.FavoritePosts)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if(user == null)
            throw new KeyNotFoundException("User not found");   

        user.AddFavoritePost(post);
        await db.SaveChangesAsync();
    }
}

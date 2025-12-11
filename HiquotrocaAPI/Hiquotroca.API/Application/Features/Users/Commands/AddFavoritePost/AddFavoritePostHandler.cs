using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.AddFavoritePost;

public class AddFavoritePostHandler(AppDbContext _db) : IRequestHandler<AddFavoritePostCommand>
{
    public async Task Handle(AddFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.Include(u => u.FavoritePosts)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
        if (user == null || post == null)
            return;

        user.AddFavoritePost(post);
        _db.Users.Update(user);

        await _db.SaveChangesAsync();
    }
}

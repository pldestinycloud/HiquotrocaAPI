using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserFavoritePosts;

public class GetUserFavoritePostsHandler(AppDbContext db) : IRequestHandler<GetUserFavoritePostsQuery, List<PostDto>>
{
    public async Task<List<PostDto>> Handle(GetUserFavoritePostsQuery request, System.Threading.CancellationToken cancellationToken)
    {
        var user = await db.Users.Include(u => u.FavoritePosts).FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null || user.FavoritePosts == null)
            return new List<PostDto>();

        return user.FavoritePosts.Select(post => MapPostToPostDto.Map(post, new PostDto())).ToList();
    }
}

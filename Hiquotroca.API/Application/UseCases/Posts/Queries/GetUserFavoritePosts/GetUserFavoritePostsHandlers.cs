using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.UseCases.Posts.Queries.GetUserFavoritePosts;

public class GetUserFavoritePostsHandlers(AppDbContext context) : IRequestHandler<GetUserFavoritePostsQuery, List<PostDto>>
{
    public async Task<List<PostDto>?> Handle(GetUserFavoritePostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await context.Users
            .Include(u => u.FavoritePosts)
            .Where(u => u.Id == request.UserId)
            .SelectMany(u => u.FavoritePosts)
            .ToListAsync();

        return posts.Select(post => MapPostToPostDto.Map(post, new PostDto())).ToList();
    }
}
using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetAllPosts;

public class GetAllPostsHandler(AppDbContext dbContext) : IRequestHandler<GetAllPostsQuery, List<PostDto>>
{
    public async Task<List<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await dbContext.Posts.ToListAsync();
        if (posts == null || !posts.Any())
            return new List<PostDto>();

        return posts.Select(post => MapPostToPostDto.Map(post, new PostDto())).ToList();
    }
}
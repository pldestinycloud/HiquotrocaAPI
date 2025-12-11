using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetPostsByIds;

public class GetPostsByIdsHandler(AppDbContext db) : IRequestHandler<GetPostsByIdsQuery, List<PostDto>>
{
    public async Task<List<PostDto>> Handle(GetPostsByIdsQuery request, CancellationToken cancellationToken)
    {
        var posts = await db.Posts.Where(p => request.PostsId.Contains(p.Id)).ToListAsync();

        if (posts == null || !posts.Any())
            return new List<PostDto>();

        return posts.Select(post => MapPostToPostDto.Map(post, new PostDto())).ToList();
    }
}
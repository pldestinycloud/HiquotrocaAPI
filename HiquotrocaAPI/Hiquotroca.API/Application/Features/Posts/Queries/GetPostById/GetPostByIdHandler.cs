using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetPostById;

public class GetPostByIdHandler(AppDbContext db) : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (post == null) return null;

        post.IncrementViewCounter();
        await db.SaveChangesAsync();

        return MapPostToPostDto.Map(post, new PostDto());
    }
}
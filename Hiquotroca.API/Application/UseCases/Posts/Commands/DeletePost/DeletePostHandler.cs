using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Posts.Commands.DeletePost;

public class DeletePostHandler(AppDbContext dbContext) : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (post == null)
            return;

        dbContext.Posts.Remove(post);
        await dbContext.SaveChangesAsync();
    }
}
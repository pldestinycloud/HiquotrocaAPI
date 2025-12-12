using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Posts.ValueObjects;
using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Posts.Commands.CreatePost;

public class CreatePostHandler(AppDbContext dbContext) : IRequestHandler<CreatePostCommand>
{
    public async Task Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var postLocation = new PostLocation(
            command.Location_City,
            command.Location_PostalCode,
            command.Location_CountryId,
            command.Location_Latitude,
            command.Location_Longitude,
            command.Location_DeliveryRadiusKm);

        var postTaxonomy = new PostTaxonomy(
            command.ActionTypeId,
            command.CategoryId,
            command.SubCategoryId);

        var postAdditionalData = new PostAdditionalData(
            command.AdditionalInfo_Elements,
            command.AdditionalInfo_Caracteristics,
            command.AdditionalInfo_Duration);

        var post = new Post(
            command.Title,
            command.Description,
            command.UserId,
            command.Images,
            postTaxonomy,
            postLocation,
            postAdditionalData);

        await dbContext.Posts.AddAsync(post);
        await dbContext.SaveChangesAsync();
    }
}
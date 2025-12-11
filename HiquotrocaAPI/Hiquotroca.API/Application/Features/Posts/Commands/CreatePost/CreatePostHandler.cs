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
    public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreatePostDto;

        var postLocation = new PostLocation(
            dto.Location.City,
            dto.Location.PostalCode,
            dto.Location.CountryId,
            dto.Location.Latitude,
            dto.Location.Longitude,
            dto.Location.DeliveryRadiusKm);

        var postTaxonomy = new PostTaxonomy(
            dto.ActionTypeId,
            dto.CategoryId,
            dto.SubCategoryId);

        var postAdditionalData = new PostAdditionalData(
            dto.AdditionalInfo!.Elements,
            dto.AdditionalInfo.Caracteristics,
            dto.AdditionalInfo.Duration);

        var post = new Post(
            dto.Title,
            dto.Description,
            dto.UserId,
            dto.Images,
            postTaxonomy,
            postLocation,
            postAdditionalData);

        await dbContext.Posts.AddAsync(post);
        await dbContext.SaveChangesAsync();
    }
}
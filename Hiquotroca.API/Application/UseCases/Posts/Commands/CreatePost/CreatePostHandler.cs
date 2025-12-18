using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Posts.ValueObjects;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Posts.Commands.CreatePost;

public class CreatePostHandler(AppDbContext dbContext) : IRequestHandler<CreatePostCommand>
{
    public async Task Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>().FindAsync(command.UserId);

        if(user == null)
            throw new KeyNotFoundException($"User with Id {command.UserId} not found an Post requires an owner.");

        var post = CreatePostFromCommand(command);

        var postPrice = post.Images.Count(); // We have a price of 1 hiquocoin per image
        
        if(!user.HaveEnoughCredits(postPrice))
            throw new InvalidOperationException($"User with Id {command.UserId} does not have enough hiquocoins to create this post. Required: {postPrice}, Available: {user.HiquoCredits}");

        user.DeducteCredits(postPrice);

        dbContext.Users.Update(user);
        await dbContext.Posts.AddAsync(post);
        await dbContext.SaveChangesAsync();
    }

    private Post CreatePostFromCommand(CreatePostCommand command)
    {
        var postLocation = new PostLocation(
            command.Location_City,
            command.Location_PostalCode,
            command.Location_Country,
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
       
        return new Post(
            command.Title,
            command.Description,
            command.UserId,
            command.Images,
            postTaxonomy,
            postLocation,
            postAdditionalData);
    }
}
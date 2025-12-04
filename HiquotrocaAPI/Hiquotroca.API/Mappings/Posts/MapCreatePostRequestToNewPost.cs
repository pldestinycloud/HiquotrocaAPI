using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Posts.Requests;
using Microsoft.Extensions.Hosting;

namespace Hiquotroca.API.Mappings.Posts
{
    public static class MapCreatePostRequestToNewPost
    {
        public static Post Map(CreatePostRequest createPostRequest)
        {
            Post post = new Post(
                 title: createPostRequest.Title,
                 description: createPostRequest.Description,
                 userId: createPostRequest.UserId,
                 actionTypeId: createPostRequest.SelectedSaleType,
                 categoryId: createPostRequest.SelectedCategory,
                 subcategoryId: createPostRequest.SelectedSubcategory,
                 elements: createPostRequest.Elements,
                 caracteristics: createPostRequest.Caracteristics,
                 duration: createPostRequest.PostDuration);

            if (createPostRequest.Localization != null)
            {
                post.UpsertPostLocation(
                    address: createPostRequest.Localization.Address,
                    city: createPostRequest.Localization.City,
                    postalCode: createPostRequest.Localization.PostalCode,
                    countryId: createPostRequest.Localization.CountryId,
                    latitude: createPostRequest.Localization.Latitude,
                    longitude: createPostRequest.Localization.Longitude,
                    deliveryRadiusKm: createPostRequest.Localization.DeliveryRadiusKm);
            }
            return post;
        }
    }
}

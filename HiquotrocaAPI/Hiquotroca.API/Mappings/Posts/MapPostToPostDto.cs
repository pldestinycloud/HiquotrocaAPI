using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.DTOs.Posts;

namespace Hiquotroca.API.Mappings.Posts;

public static class MapPostToPostDto
{
    public static PostDto Map(Post post, PostDto dto)
    {
        dto.Id = post.Id;
        dto.Title = post.Title;
        dto.Description = post.Description;
        dto.UserId = post.UserId;
        dto.ActionTypeId = post.PostTaxonomyData.ActionTypeId;
        dto.CategoryId = post.PostTaxonomyData.CategoryId; 
        dto.SubcategoryId = post.PostTaxonomyData.SubcategoryId;
        dto.IsActive = post.IsActive;
        dto.Images = post.Images;
        dto.Location = new PostLocationDto
        {
            City = post.Location.City,
            PostalCode = post.Location.PostalCode,
            CountryId = post.Location.CountryId,
            Latitude = post.Location.Latitude,
            Longitude = post.Location.Longitude,
            DeliveryRadiusKm = post.Location.DeliveryRadiusKm
        };
        dto.AdditionalInfo = new PostAdditionalInfoDto
        {
            Duration = post.AdditionalData.Duration,
            ViewCounter = post.AdditionalData.ViewCounter,
            IsFeatured = post.AdditionalData.IsFeatured,
            Elements = post.AdditionalData.Elements,
            Caracteristics = post.AdditionalData.Caracteristics
        };

        return dto;
    }
}

using Hiquotroca.API.Domain.Entities;

namespace Hiquotroca.API.DTOs.Posts.Requests
{
    public class CreatePostDto
    {
        public string Title { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
        public long UserId { get; set; }
        public long ActionTypeId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public required PostLocationDto? Location { get; set; }
        public required PostAdditionalInfoDto? AdditionalInfo { get; set; }
    }

    public class CreatePostLocationDto
    {
        public string? Address { get; set; }
        public string City { get; set; } = string.Empty!;
        public string? PostalCode { get; set; }
        public long CountryId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? DeliveryRadiusKm { get; set; }
    }

    public class CreatePostAdditionalInfoDto
    {
        public string? Elements { get; set; }
        public string? Caracteristics { get; set; }
        public int? Duration { get; set; }
    }
}

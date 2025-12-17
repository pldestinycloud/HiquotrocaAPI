namespace Hiquotroca.API.DTOs.Posts
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long UserId { get; set; }
        public long ActionTypeId { get; set; }
        public long CategoryId { get; set; }
        public long SubcategoryId { get; set; }
        public bool IsActive { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public PostLocationDto? Location { get; set; } 
        public PostAdditionalInfoDto? AdditionalInfo { get; set; }
    }

    public class PostLocationDto
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? DeliveryRadiusKm { get; set; }
    }

    public class PostAdditionalInfoDto
    {
        public string? Elements { get; set; }
        public string? Caracteristics { get; set; }
        public int? Duration { get; set; }
        public int ViewCounter { get; set; }
        public bool IsFeatured { get; set; }
    }
}
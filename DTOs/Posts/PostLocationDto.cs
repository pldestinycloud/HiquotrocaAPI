namespace Hiquotroca.API.DTOs.Posts
{
    public class PostLocationDto
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public long? CountryId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? DeliveryRadiusKm { get; set; }
    }
}

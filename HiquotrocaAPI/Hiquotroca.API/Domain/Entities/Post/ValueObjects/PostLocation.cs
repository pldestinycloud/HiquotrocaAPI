using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities.Posts.ValueObjects
{
    public class PostLocation 
    {
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public long CountryId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? DeliveryRadiusKm { get; set; }

        public PostLocation(string? city, string? postalCode, long countryId, double? latitude, double? longitude, int? deliveryRadiusKm)
        {
            City = city;
            PostalCode = postalCode;
            CountryId = countryId;
            Latitude = latitude;
            Longitude = longitude;
            DeliveryRadiusKm = deliveryRadiusKm;
        }
    }
}

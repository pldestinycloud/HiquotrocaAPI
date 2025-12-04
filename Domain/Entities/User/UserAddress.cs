using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities.Users
{
    public class UserAddress
    {
        public long Id { get; private set;  }
        public string Address { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string? PostalCode { get; private set; }
        public long CountryId { get; private set; }

        public UserAddress(string address, string city, string? postalCode, long countryId)
        {
            Address = address;
            City = city;
            PostalCode = postalCode;
            CountryId = countryId;
        }
    }
}

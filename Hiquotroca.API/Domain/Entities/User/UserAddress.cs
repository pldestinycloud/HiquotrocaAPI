using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities.Users
{
    public class UserAddress
    {
        public string Address { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string? PostalCode { get; private set; }
        public string Country { get; private set; }

        public UserAddress(string address, string city, string? postalCode, string country)
        {
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}

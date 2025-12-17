namespace Hiquotroca.API.DTOs.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int HiquoCredits { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserAddressDto? Address { get; set; }
    }

    public class UserAddressDto
    {
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public long CountryId { get; set; }
    }
}

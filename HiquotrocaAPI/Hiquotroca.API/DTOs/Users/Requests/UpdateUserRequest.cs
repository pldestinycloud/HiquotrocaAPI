namespace Hiquotroca.API.DTOs.Users.Requests
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public UpdateUserAddressDto? Address { get; set; }
    }

    public class UpdateUserAddressDto
    {
        public string Address { get; set; } = string.Empty!;
        public string City { get; set; } = string.Empty!;
        public string? PostalCode { get; set; } = string.Empty;
        public long CountryId { get; set; }
    }
}

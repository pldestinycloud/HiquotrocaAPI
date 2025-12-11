using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.User;

namespace Hiquotroca.API.Mappings.Users;

public static class MapUserToUserDto
{
    public static UserDto Map(User user, UserDto userDto)
    {
        userDto.Id = user.Id;
        userDto.FirstName = user.FirstName;
        userDto.LastName = user.LastName;
        userDto.Email = user.Email;
        userDto.PhoneNumber = user.PhoneNumber;
        userDto.BirthDate = user.BirthDate;
        userDto.Address = new UserAddressDto
        {
            Address = user.Address?.Address,
            City = user.Address?.City,
            PostalCode = user.Address?.PostalCode,
            CountryId = user.Address?.CountryId ?? 0    
        };

        return userDto;
    }
}

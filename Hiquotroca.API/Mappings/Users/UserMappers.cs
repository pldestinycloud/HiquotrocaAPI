using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Users;
using Hiquotroca.API.DTOs.Users;

namespace Hiquotroca.API.Mappings.Users;

public static class UserMappers
{
    public static UserDto MapToUserDto(User user, UserDto userDto)
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
            Country = user.Address?.Country
        };

        return userDto;
    }

    public static UserBriefDataDto MapToUserBriefDataDto(User user, UserBriefDataDto userBriefDataDto)
    {
        userBriefDataDto.Id = user.Id;
        userBriefDataDto.FirstName = user.FirstName;
        userBriefDataDto.LastName = user.LastName;
        userBriefDataDto.Email = user.Email;
        return userBriefDataDto;
    }
}

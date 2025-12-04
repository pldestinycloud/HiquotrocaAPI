using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hiquotroca.API.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<BaseResult<List<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null || !users.Any())
                return BaseResult<List<UserDto>>.Failure(new Error(ErrorCode.NotFound, "No users found"));

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Nome = user.FirstName,
                Sobrenome = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
            }).ToList();

            return BaseResult<List<UserDto>>.Ok(userDtos);
        }

        public async Task<BaseResult<UserDto>> GetUserByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return BaseResult<UserDto>.Failure(new Error(ErrorCode.NotFound, "User not found"));

            return BaseResult<UserDto>.Ok(new UserDto
            {
                Id = user.Id,
                Nome = user.FirstName,
                Sobrenome = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
            });
        }

        public async Task<BaseResult<UserDto>> UpdateUserAsync(long id, UserDto newUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> DeleteUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> AddFavoritePostAsync(long userId, long postId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> RemoveFavoritePostAsync(long userId, long postId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> FollowUserAsync(long userId, long targetUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> UnfollowUserAsync(long userId, long targetUserId)
        {
            throw new NotImplementedException();
        }
    }
}
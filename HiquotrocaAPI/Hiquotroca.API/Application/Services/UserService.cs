using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Hiquotroca.API.Mappings.Posts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hiquotroca.API.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public UserService(UserRepository userRepository, PostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<List<UserDto>?> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null || !users.Any())
                return new List<UserDto>();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Nome = user.FirstName,
                Sobrenome = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
            }).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Nome = user.FirstName,
                Sobrenome = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
            };
        }
        
        public async Task UpdateUserAsync(long id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return;

            user = user.UpdateUser(updateUserDto.FirstName,
                updateUserDto.LastName,
                updateUserDto.PhoneNumber,
                updateUserDto.BirthDate
            );

            if(updateUserDto.Address != null)
            {
                user = user.SetUserAddress(
                    updateUserDto.Address.Address,
                    updateUserDto.Address.City,
                    updateUserDto.Address.PostalCode,
                    updateUserDto.Address.CountryId
                );
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return;

            await _userRepository.DeleteAsync(user);
        }

        public async Task AddFavoritePostAsync(long userId, long postId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return;

            user.AddFavoritePost(postId);
            await _userRepository.UpdateAsync(user);
        }

        public async Task RemoveFavoritePostAsync(long userId, long postId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return;
            user.RemoveFavoritePost(postId);

            await _userRepository.UpdateAsync(user);

        }

        public async Task FollowUserAsync(long userId, long targetUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return;

            user.StartFollowing(targetUserId);
            await _userRepository.UpdateAsync(user);
        }

        public async Task UnfollowUserAsync(long userId, long targetUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return;

            user.StopFollowing(targetUserId);
            await _userRepository.UpdateAsync(user);
        }

        internal async Task AttributePromotionalCode(long userId, long promoCodeId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return;

            user.AddPromotionalCode(promoCodeId);
            await _userRepository.UpdateAsync(user);
        }

        internal async Task RemovePromotionalCode(long userId, long promoCodeId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return;

            user.RemovePromotionalCode(promoCodeId);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<List<long>> GetUserFavoritePostsAsync(long userId)
        {
            return await _userRepository.GetUserFavoritePostsAsync(userId);
        }

        public async Task<List<UserDto>?> GetUserFollowersAsync(long userId)
        {
            var followingUsersIds = await _userRepository.GetFollowingUsersForUserAsync(userId);
            if (followingUsersIds == null || !followingUsersIds.Any())
                return null;

            var followingUsers = await _userRepository.GetUsersByIdsAsync(followingUsersIds);

            return followingUsers!.Select(user => new UserDto
            {
                Id = user.Id,
                Nome = user.FirstName,
                Sobrenome = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
            }).ToList();
        }

        internal async Task<List<long>> GetUserPromotionalCodesAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            return user.PromotionalCodes;
        }
    }
}
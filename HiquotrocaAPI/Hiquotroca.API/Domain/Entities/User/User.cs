using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = string.Empty;
        public string? PasswordHash { get; private set; }
        public string? PhoneNumber { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string? LastName { get; private set; }
        public string? ResetToken { get; private set; }
        public DateTime? ResetTokenExpiry { get; private set; }

        public UserAddress? Address { get; private set; }
        public List<long> FavoritePosts { get; private set; }
        public List<long> FollowingUsers { get; private set; }
        public long? PromotionalCodeId { get; private set; }
        public PromotionalCode? PromotionalCode { get; private set; }

        public User(string email, string firstName, string lastName, string? phoneNumber = null, DateTime? birthDate = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            FavoritePosts = new List<long>();
            FollowingUsers = new List<long>();
        }

        public User UpdateUser(string firstName, string? lastName, string? phoneNumber, DateTime? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            return this;
        }

        public User UpdateUserPassword(string passwordHash)
        {
            this.PasswordHash = passwordHash;
            return this;
        }

        public User UpsertUserAddress(string address, string city, string? postalCode, long countryId)
        {
            this.Address = new UserAddress(address, city, postalCode, countryId);

            return this;
        }

        public void DeleteUser()
        {
            // Implement any logic needed before deleting a user, if necessary.
            // Such as validations or logging.
        }

        public User StartFollowing(long targetFollowerId)
        {
            if(this.FollowingUsers == null)
            {
                this.FollowingUsers = new List<long>();
            }

            if(this.FollowingUsers.Any(f => f == targetFollowerId))
            {
                // Already following this user
                return this;
            }
            this.FollowingUsers.Add(targetFollowerId);
            return this;
        }

        public User StopFollowing(long targetFollowerId)
        {
            if(this.FollowingUsers == null)
            {
                this.FollowingUsers = new List<long>();
            }
            var follower = this.FollowingUsers.FirstOrDefault(f => f == targetFollowerId);
            if(follower == 0)
            {
                // Not following this user
                return this;
            }
            this.FollowingUsers.Remove(follower);
            return this;
        }

        public User AddFavoritePost(long postId)
        {
            if(this.FavoritePosts == null)
            {
                this.FavoritePosts = new List<long>();
            }
            if(this.FavoritePosts.Any(p => p == postId))
            {
                // Post already favorited
                return this;
            }
            this.FavoritePosts.Add(postId);
            return this;
        }

        public User RemoveFavoritePost(long postId)
        {
            if(this.FavoritePosts == null)
            {
                this.FavoritePosts = new List<long>();
            }
            var post = this.FavoritePosts.FirstOrDefault(p => p == postId);
            if(post == 0)
            {
                // Post not in favorites
                return this;
            }
            this.FavoritePosts.Remove(post);
            return this;
        }
    }    
}

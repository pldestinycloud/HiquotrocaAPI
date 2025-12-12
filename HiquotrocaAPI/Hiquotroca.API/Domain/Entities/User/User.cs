using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Posts;

namespace Hiquotroca.API.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        //User Personal Info 
        public string FirstName { get; private set; } = string.Empty;
        public string? LastName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public double HiquoCredits { get; set; } = 0.0;

        //User Auth Info
        public string Email { get; private set; } = string.Empty;
        public string? PasswordHash { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiry { get; private set; }

        //User Address Info
        public UserAddress? Address { get; private set; }

        //User Relations
        public List<Post> FavoritePosts { get; private set; }
        public List<User> FollowingUsers { get; private set; } = new List<User>();
        public List<PromotionalCode> PromotionalCodes { get; private set; } = new List<PromotionalCode>();

        private User() { }
        public User(string email, string firstName, string lastName, string? phoneNumber = null, DateTime? birthDate = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
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

        public User SetUserAddress(string address, string city, string? postalCode, long countryId)
        {
            this.Address = new UserAddress(address, city, postalCode, countryId);

            return this;
        }

        public void DeleteUser()
        {
            // Implement any logic needed before deleting a user, if necessary.
            // Such as validations or logging.
        }

        public User StartFollowing(User userToFollow)
        {
            if (this.FollowingUsers.Contains(userToFollow))
                return this;

            this.FollowingUsers.Add(userToFollow);
            return this;
        }

        public User StopFollowing(User userToUnfollow)
        {
            if (!this.FollowingUsers.Contains(userToUnfollow))
                return this;

            this.FollowingUsers.Remove(userToUnfollow);
            return this;
        }

        public User AddFavoritePost(Post post)
        {
            if (this.FavoritePosts.Contains(post))
                return this;

            this.FavoritePosts.Add(post);
            return this;
        }

        public User RemoveFavoritePost(Post post)
        {
            if (!this.FavoritePosts.Contains(post))
                return this;

            this.FavoritePosts.Remove(post);
            return this;
        }

        public User AddPromotionalCode(PromotionalCode promoCode)
        {
            if (this.PromotionalCodes.Contains(promoCode))
                return this;

            this.PromotionalCodes.Add(promoCode);
      
            return this;
        }

        public User RemovePromotionalCode(PromotionalCode promoCode)
        {
            if (!this.PromotionalCodes.Contains(promoCode))
                return this;

            this.PromotionalCodes.Remove(promoCode);
            return this;
        }

        //Isto Tá muito mal aqui mas pronto. Um dos motivos pelos quais o modelo de negócio nao deve ser misturado
        //com questoes de segurança 
        //No Futuro ao segregar as responsabilidades eliminar este metodo
        public User UpdateRefreshToken(string refreshToken, DateTime expiry)
        {
            this.RefreshToken = refreshToken;
            this.RefreshTokenExpiry = expiry;
            return this;
        }
    }
}
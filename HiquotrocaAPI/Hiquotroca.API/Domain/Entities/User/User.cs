using Hiquotroca.API.Domain.Common;

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
        public List<long> FavoritePosts { get; private set; }
        public List<long> FollowingUsers { get; private set; }
        public List<long> PromotionalCodes { get; private set; }

        public User(string email, string firstName, string lastName, string? phoneNumber = null, DateTime? birthDate = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            FavoritePosts = new List<long>();
            FollowingUsers = new List<long>();
            PromotionalCodes = new List<long>();
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

        public User StartFollowing(long targetFollowerId)
        {
            if (this.FollowingUsers.Contains(targetFollowerId))
                return this;
            
            this.FollowingUsers.Add(targetFollowerId);
            return this;
        }

        public User StopFollowing(long targetFollowerId)
        {
            if(!this.FollowingUsers.Contains(targetFollowerId))
                return this;

            this.FollowingUsers.Remove(targetFollowerId);
            return this;
        }

        public User AddFavoritePost(long postId)
        {
            if (this.FavoritePosts.Contains(postId))
                return this;

            this.FavoritePosts.Add(postId);
            return this;
        }

        public User RemoveFavoritePost(long postId)
        {
            if (!this.FavoritePosts.Contains(postId))
                return this;

            this.FavoritePosts.Remove(postId);
            return this;
        }

        public User AddPromotionalCode(long promoCodeId)
        {
            if (this.PromotionalCodes.Contains(promoCodeId))
                return this;

            this.PromotionalCodes.Add(promoCodeId);
            return this;
        }

        public User RemovePromotionalCode(long promoCodeId)
        {
            if (!this.PromotionalCodes.Contains(promoCodeId))
                return this;

            this.PromotionalCodes.Remove(promoCodeId);
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
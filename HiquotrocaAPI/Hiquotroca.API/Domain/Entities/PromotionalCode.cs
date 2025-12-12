using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Domain.Entities
{
    public class PromotionalCode : BaseEntity
    {
        public string Code { get; private set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }
        public bool IsActive { get; private set; }
        public List<User> Owners { get; private set; }

        public PromotionalCode(string code, DateTime expiryDate)
        {
            Code = code;
            ExpiryDate = expiryDate;
            IsActive = true;
        }

        public PromotionalCode Update(string code, DateTime expiryDate, bool isActive)
        {
            Code = code;
            ExpiryDate = expiryDate;
            IsActive = isActive;

            return this;
        }

        public PromotionalCode AssignUser(User user)
        {
            if(!Owners.Contains(user))
                Owners.Add(user);

            return this;
        } 

        public PromotionalCode RemoveUser(User user)
        {
            if(Owners.Contains(user))
                Owners.Remove(user);

            return this;
        }
    }
}

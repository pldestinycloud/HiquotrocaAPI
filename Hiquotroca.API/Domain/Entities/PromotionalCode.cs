using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Domain.Entities
{
    public class PromotionalCode : BaseEntity
    {
        public string Code { get; private set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }
        public bool IsActive { get; private set; }
        public double BonusPercentage { get; private set; }
        public List<User> Owners { get; private set; }

        public PromotionalCode(string code, DateTime expiryDate, double bonusPercentage)
        {
            Code = code;
            ExpiryDate = expiryDate;
            IsActive = true;
            BonusPercentage = bonusPercentage;
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

        public bool IsValid()
        {
            return IsActive && ExpiryDate >= DateTime.UtcNow;
        }

        public bool HasAlreadyBeenUsedByUser(User user)
        {
            return Owners.Contains(user);
        }

        public double CalculateBonus(double amount)
        {
            return amount * (BonusPercentage / 100);
        }
    }
}

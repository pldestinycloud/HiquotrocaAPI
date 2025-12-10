using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Domain.Entities
{
    public class PromotionalCode : BaseEntity
    {
        public string Code { get; private set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }
        public bool IsActive { get; private set; }
        public List<User> OwnersId { get; private set; }

        public PromotionalCode(string code, DateTime expiryDate)
        {
            Code = code;
            ExpiryDate = expiryDate;
            IsActive = true;
        }
    }
}

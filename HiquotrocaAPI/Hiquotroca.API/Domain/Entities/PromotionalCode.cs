using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Domain.Entities
{
    public class PromotionalCode : BaseEntity
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public long? UserId { get; set; } // relation 1:1 with User
        public User? User { get; set; }
    }
}

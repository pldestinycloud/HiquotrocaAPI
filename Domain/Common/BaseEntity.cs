using Hiquotroca.API.Domain.Entities;

namespace Hiquotroca.API.Domain.Common
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

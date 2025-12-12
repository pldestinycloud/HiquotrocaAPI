namespace Hiquotroca.API.DTOs.PromotionalCode
{
    public class PromotionalCodeDto
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}

namespace Hiquotroca.API.DTOs.PromotionalCode
{
    public class CreatePromotionalCodeDto
    {
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public long UserId { get; set; }
    }

    public class UpdatePromotionalCodeDto
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }

}

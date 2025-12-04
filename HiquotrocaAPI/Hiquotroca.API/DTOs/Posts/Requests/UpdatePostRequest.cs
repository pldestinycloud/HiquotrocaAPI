namespace Hiquotroca.API.DTOs.Posts.Requests
{
    public class UpdatePostRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long ActionTypeId { get; set; }
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? LocationId { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
    }
}

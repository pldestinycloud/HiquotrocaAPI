namespace Hiquotroca.API.DTOs.Posts
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long UserId { get; set; }
        public long ActionTypeId { get; set; }
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? LocationId { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
    }
}
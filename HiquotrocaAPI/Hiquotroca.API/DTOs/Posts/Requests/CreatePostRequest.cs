using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.DTOs.Post;

namespace Hiquotroca.API.DTOs.Posts.Requests
{
    public class CreatePostRequest
    {
        public int UserId { get; set; }
        public int SelectedSaleType { get; set; }
        public List<ImageDto>? ImageUrls { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public int SelectedCategory { get; set; } = 0!;
        public int SelectedSubcategory { get; set; } = 0!;
        public string Description { get; set; } = string.Empty;
        public string? Caracteristics { get; set; }
        public string? Elements { get; set; }
        public PostLocationDto? Localization { get; set; }
        public int? PostDuration { get; set; }
    }
}

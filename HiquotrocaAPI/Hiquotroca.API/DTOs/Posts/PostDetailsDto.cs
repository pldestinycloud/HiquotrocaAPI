using System;
using System.Collections.Generic;

namespace Hiquotroca.API.DTOs.Post
{
    public class PostDetailsDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ActionTypeName { get; set; } = string.Empty;
        public List<ImageDto> Images { get; set; } = new();
    }
}

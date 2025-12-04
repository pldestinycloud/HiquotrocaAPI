namespace Hiquotroca.API.DTOs.Post
{
    public class ImageDto
    {
        public long Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsPrimary { get; set; } = false;
    }
}

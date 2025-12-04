namespace Hiquotroca.API.DTOs.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Sobrenome { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

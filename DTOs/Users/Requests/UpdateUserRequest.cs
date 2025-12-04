namespace Hiquotroca.API.DTOs.Users.Requests
{
    public class UpdateUserRequest
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}

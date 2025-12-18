namespace Hiquotroca.API.DTOs.Auth.Responses
{
    public class LoginResponse
    {
        public string Email { get; set; } = string.Empty;
        public long UserId { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}

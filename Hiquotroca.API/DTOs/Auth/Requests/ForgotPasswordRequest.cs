using System.ComponentModel.DataAnnotations;

namespace Hiquotroca.API.DTOs.Auth.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}


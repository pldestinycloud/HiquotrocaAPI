namespace Hiquotroca.API.DTOs.Users;

public class UserBriefDataDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Email { get; set; } = string.Empty;
}

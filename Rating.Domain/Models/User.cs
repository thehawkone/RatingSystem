using Rating.Domain.Enums;

namespace Rating.Domain.Models;

public class User
{
    public const int MaxUserNameLength = 50;
    
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
}
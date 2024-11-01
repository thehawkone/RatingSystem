using Rating.Domain.Enums;

namespace Rating.Application.DTOs.User;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}
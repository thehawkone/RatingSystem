namespace Rating.Application.DTOs.User;

public class ChangePasswordDto : UserLoginDto
{
    public string NewPassword { get; set; }
}
namespace Rating.Application.DTOs;

public class ChangePasswordDto : UserLoginDto
{
    public string NewPassword { get; set; }
}
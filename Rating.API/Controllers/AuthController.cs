using Microsoft.AspNetCore.Mvc;
using Rating.Application.DTOs;
using Rating.Application.DTOs.User;
using Rating.Application.Services;

namespace Rating.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public AuthController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var user = await _userService.RegisterAsync(userRegisterDto);
        return Ok("Пользователь успешно зарегистрирован");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userService.LoginAsync(userLoginDto.Name, userLoginDto.Password);
        
        var token = _tokenService.GenerateToken(user);
        return Ok($"Авторизация прошла успешно!\nВаш токен: {token}");
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(Guid userId, ChangePasswordDto changePasswordDto)
    {
        await _userService.ChangePasswordAsync(userId, changePasswordDto.Password, changePasswordDto.NewPassword);
        return Ok("Пароль успешно изменён");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        await _userService.DeleteAsync(userId);
        return Ok("Пользователь успешно удалён");
    }
}
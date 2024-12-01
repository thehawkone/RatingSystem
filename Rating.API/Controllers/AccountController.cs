using Microsoft.AspNetCore.Mvc;
using Rating.Application.DTOs;
using Rating.Application.DTOs.User;
using Rating.Application.Services;
using Rating.Domain.Abstractions;
using Rating.Domain.Enums;

namespace Rating.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AccountController(UserService userService, TokenService tokenService, IUserRepository userRepository)
    {
        _userService = userService;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        await _userService.RegisterAsync(userRegisterDto);
        return Ok("Пользователь успешно зарегистрирован");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = await _userService.LoginAsync(userLoginDto.Name, userLoginDto.Password);
        return Ok($"Авторизация прошла успешно!\n\nВаш токен: {token}");
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
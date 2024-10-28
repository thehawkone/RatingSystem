using Rating.Application.DTOs;
using Rating.Application.DTOs.User;
using Rating.DataAccess;
using Rating.Domain.Abstractions;
using Rating.Domain.Models;

namespace Rating.Application.Services;

public class UserService
{
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly RatingDbContext _ratingDbContext;

    public UserService(IUserRepository userRepository, RatingDbContext ratingDbContext, TokenService tokenService)
    {
        _userRepository = userRepository;
        _ratingDbContext = ratingDbContext;
        _tokenService = tokenService;
    }

    public async Task<UserDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var passwordHash = HashPassword(userRegisterDto.Password);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = userRegisterDto.Name,
            PasswordHash = passwordHash,
            Email = userRegisterDto.Email
        };
        
        await _userRepository.CreateUserAsync(user);
        await _ratingDbContext.SaveChangesAsync();

        return new UserDto { UserId = Guid.NewGuid(), Name = user.Name, Email = user.Email };
    }

    public async Task<string> LoginAsync(string name, string password)
    {
        var user = await _userRepository.GetUserByName(name);
        if (!VerifyPassword(password, user.PasswordHash)) {
            throw new Exception("Пароль или логин неверный");
        }
        
        await _ratingDbContext.SaveChangesAsync();
        return _tokenService.GenerateToken(name);
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            throw new Exception("Пользователь не найден");
        }

        if (!VerifyPassword(oldPassword, user.PasswordHash)) {
            throw new Exception("Пароль не верный");
        }

        if (oldPassword == newPassword) {
            throw new Exception("Новый пароль не должен совпадать со старым");
        }
        
        var newHashedPassword = HashPassword(newPassword);
        user.PasswordHash = newHashedPassword;
        
        await _userRepository.UpdateUserAsync(user);
        await _ratingDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            throw new Exception("Неверный идентификатор пользователя");
        }

        await _userRepository.DeleteUserAsync(userId);
        await _ratingDbContext.SaveChangesAsync();
        
        return true;
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
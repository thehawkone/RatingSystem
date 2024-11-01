using Rating.DataAccess;
using Rating.Domain.Abstractions;
using Rating.Domain.Enums;

namespace Rating.Application.Services;

public class AdminService
{
    private readonly IUserRepository _userRepository;
    private readonly RatingDbContext _ratingDbContext;

    public AdminService(IUserRepository userRepository, RatingDbContext ratingDbContext)
    {
        _userRepository = userRepository;
        _ratingDbContext = ratingDbContext;
    }
    
    public async Task ReplaceRoleAsync(Guid userId, Role role)
    {
        if (role != Role.Admin & role != Role.User) {
            throw new Exception("Неправильно введенная роль");
        }
        
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null) {
            throw new Exception("Пользователь не найден");
        }

        if (role == user.Role) {
            throw new Exception("Новая роль не должна совпадать со старой");
        }
        
        user.Role = role;
        
        await _userRepository.UpdateUserAsync(user);
        await _ratingDbContext.SaveChangesAsync();
    }
}
using Rating.Domain.Models;

namespace Rating.Domain.Abstractions;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid userId);
}
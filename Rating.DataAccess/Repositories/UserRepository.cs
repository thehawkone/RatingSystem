using Microsoft.EntityFrameworkCore;
using Rating.Domain.Abstractions;
using Rating.Domain.Enums;
using Rating.Domain.Models;

namespace Rating.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RatingDbContext _context;

    public UserRepository(RatingDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> GetUserByName(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<Role> GetRoleByName(string name)
    {
        var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Name == name);
        
        // Если пользователь найден, возвращаем его роль, иначе возвращаем роль по умолчанию
        return user?.Role ?? Role.User; 
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null) {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
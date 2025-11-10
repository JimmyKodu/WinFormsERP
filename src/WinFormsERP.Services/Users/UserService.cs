using Microsoft.EntityFrameworkCore;
using WinFormsERP.Core.Entities;
using WinFormsERP.Core.Interfaces.Services;
using WinFormsERP.Data.Context;

namespace WinFormsERP.Services.Users;

/// <summary>
/// User service implementation
/// </summary>
public class UserService : IUserService
{
    private readonly ERPDbContext _context;

    public UserService(ERPDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> AssignRoleAsync(int userId, int roleId)
    {
        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveRoleAsync(int userId, int roleId)
    {
        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}

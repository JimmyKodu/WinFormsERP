using Microsoft.EntityFrameworkCore;
using WinFormsERP.Core.Entities;
using WinFormsERP.Data.Context;
using WinFormsERP.Services.Users;

namespace WinFormsERP.Tests.Services;

public class UserServiceTests : IDisposable
{
    private readonly ERPDbContext _context;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ERPDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ERPDbContext(options);
        _userService = new UserService(_context);
    }

    [Fact]
    public async Task CreateUserAsync_CreatesUser()
    {
        // Arrange
        var user = new User
        {
            Username = "newuser",
            PasswordHash = "hash",
            Email = "new@test.com",
            FullName = "New User"
        };

        // Act
        var result = await _userService.CreateUserAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("newuser", result.Username);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsCorrectUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "hash",
            Email = "test@test.com",
            FullName = "Test User"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.GetUserByIdAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal("testuser", result.Username);
    }

    [Fact]
    public async Task GetUserByUsernameAsync_ReturnsCorrectUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "hash",
            Email = "test@test.com",
            FullName = "Test User"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.GetUserByUsernameAsync("testuser");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsAllActiveUsers()
    {
        // Arrange
        var user1 = new User
        {
            Username = "user1",
            PasswordHash = "hash",
            Email = "user1@test.com",
            FullName = "User 1"
        };
        var user2 = new User
        {
            Username = "user2",
            PasswordHash = "hash",
            Email = "user2@test.com",
            FullName = "User 2"
        };
        _context.Users.AddRange(user1, user2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteUserAsync_SoftDeletesUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "hash",
            Email = "test@test.com",
            FullName = "Test User"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        await _userService.DeleteUserAsync(user.Id);

        // Assert
        var deletedUser = await _context.Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.NotNull(deletedUser);
        Assert.True(deletedUser.IsDeleted);
    }

    [Fact]
    public async Task AssignRoleAsync_AssignsRoleToUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "hash",
            Email = "test@test.com",
            FullName = "Test User"
        };
        var role = new Role
        {
            Name = "TestRole",
            Description = "Test Role"
        };
        _context.Users.Add(user);
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.AssignRoleAsync(user.Id, role.Id);

        // Assert
        Assert.True(result);
        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
        Assert.NotNull(userRole);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

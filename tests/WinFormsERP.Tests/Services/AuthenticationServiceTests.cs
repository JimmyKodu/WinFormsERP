using Microsoft.EntityFrameworkCore;
using WinFormsERP.Core.Entities;
using WinFormsERP.Data.Context;
using WinFormsERP.Services.Authentication;

namespace WinFormsERP.Tests.Services;

public class AuthenticationServiceTests : IDisposable
{
    private readonly ERPDbContext _context;
    private readonly AuthenticationService _authService;

    public AuthenticationServiceTests()
    {
        var options = new DbContextOptionsBuilder<ERPDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ERPDbContext(options);
        _authService = new AuthenticationService(_context);
    }

    [Fact]
    public async Task AuthenticateAsync_WithValidCredentials_ReturnsUser()
    {
        // Arrange
        var password = "password123";
        var user = new User
        {
            Username = "testuser",
            PasswordHash = _authService.HashPassword(password),
            Email = "test@test.com",
            FullName = "Test User",
            IsActive = true
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _authService.AuthenticateAsync("testuser", password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.NotNull(result.LastLoginAt);
    }

    [Fact]
    public async Task AuthenticateAsync_WithInvalidPassword_ReturnsNull()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = _authService.HashPassword("password123"),
            Email = "test@test.com",
            FullName = "Test User",
            IsActive = true
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _authService.AuthenticateAsync("testuser", "wrongpassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_WithInactiveUser_ReturnsNull()
    {
        // Arrange
        var password = "password123";
        var user = new User
        {
            Username = "testuser",
            PasswordHash = _authService.HashPassword(password),
            Email = "test@test.com",
            FullName = "Test User",
            IsActive = false
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _authService.AuthenticateAsync("testuser", password);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ChangePasswordAsync_WithValidOldPassword_ReturnsTrue()
    {
        // Arrange
        var oldPassword = "oldpassword";
        var newPassword = "newpassword";
        var user = new User
        {
            Username = "testuser",
            PasswordHash = _authService.HashPassword(oldPassword),
            Email = "test@test.com",
            FullName = "Test User",
            IsActive = true
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _authService.ChangePasswordAsync(user.Id, oldPassword, newPassword);

        // Assert
        Assert.True(result);
        var updatedUser = await _context.Users.FindAsync(user.Id);
        Assert.NotNull(updatedUser);
        Assert.True(_authService.VerifyPassword(newPassword, updatedUser.PasswordHash));
    }

    [Fact]
    public void HashPassword_ProducesSameHashForSamePassword()
    {
        // Arrange
        var password = "testpassword";

        // Act
        var hash1 = _authService.HashPassword(password);
        var hash2 = _authService.HashPassword(password);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "testpassword";
        var hash = _authService.HashPassword(password);

        // Act
        var result = _authService.VerifyPassword(password, hash);

        // Assert
        Assert.True(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

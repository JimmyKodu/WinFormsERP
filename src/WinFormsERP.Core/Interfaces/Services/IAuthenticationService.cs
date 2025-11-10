using WinFormsERP.Core.Entities;

namespace WinFormsERP.Core.Interfaces.Services;

/// <summary>
/// Authentication service interface
/// </summary>
public interface IAuthenticationService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

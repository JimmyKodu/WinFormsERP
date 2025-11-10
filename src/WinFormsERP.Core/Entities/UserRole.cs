namespace WinFormsERP.Core.Entities;

/// <summary>
/// Many-to-many relationship between Users and Roles
/// </summary>
public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}

namespace WinFormsERP.Core.Entities;

/// <summary>
/// Role entity for role-based access control
/// </summary>
public class Role : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

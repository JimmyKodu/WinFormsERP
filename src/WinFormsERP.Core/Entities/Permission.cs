namespace WinFormsERP.Core.Entities;

/// <summary>
/// Permission entity for granular access control
/// </summary>
public class Permission : BaseEntity
{
    public required string Name { get; set; }
    public required string Module { get; set; }
    public string? Description { get; set; }
    
    // Navigation properties
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

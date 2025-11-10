namespace WinFormsERP.Core.Entities.Inventory;

/// <summary>
/// Warehouse entity for inventory location management
/// </summary>
public class Warehouse : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}

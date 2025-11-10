namespace WinFormsERP.Core.Entities.Inventory;

/// <summary>
/// Product entity for inventory management
/// </summary>
public class Product : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Cost { get; set; }
    public decimal ReorderLevel { get; set; }
    public decimal ReorderQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}

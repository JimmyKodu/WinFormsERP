namespace WinFormsERP.Core.Entities.Inventory;

/// <summary>
/// Product Stock entity tracking inventory levels per warehouse
/// </summary>
public class ProductStock : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    
    public decimal QuantityOnHand { get; set; }
    public decimal ReservedQuantity { get; set; }
    public decimal AvailableQuantity => QuantityOnHand - ReservedQuantity;
}

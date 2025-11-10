using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.Purchasing;

/// <summary>
/// Purchase Order entity
/// </summary>
public class PurchaseOrder : BaseEntity
{
    public required string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Today;
    public DateTime? ExpectedDeliveryDate { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public ICollection<PurchaseOrderItem> OrderItems { get; set; } = new List<PurchaseOrderItem>();
}

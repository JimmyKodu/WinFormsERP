using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.Sales;

/// <summary>
/// Sales Order entity
/// </summary>
public class SalesOrder : BaseEntity
{
    public required string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Today;
    public DateTime? DeliveryDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public ICollection<SalesOrderItem> OrderItems { get; set; } = new List<SalesOrderItem>();
}

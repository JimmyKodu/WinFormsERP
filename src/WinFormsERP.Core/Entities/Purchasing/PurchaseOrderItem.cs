using WinFormsERP.Core.Entities.Inventory;

namespace WinFormsERP.Core.Entities.Purchasing;

/// <summary>
/// Purchase Order Item entity
/// </summary>
public class PurchaseOrderItem : BaseEntity
{
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal LineTotal { get; set; }
}

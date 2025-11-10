using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.Inventory;

/// <summary>
/// Stock Transfer entity for moving inventory between warehouses
/// </summary>
public class StockTransfer : BaseEntity
{
    public required string TransferNumber { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.Today;
    public int FromWarehouseId { get; set; }
    public Warehouse FromWarehouse { get; set; } = null!;
    
    public int ToWarehouseId { get; set; }
    public Warehouse ToWarehouse { get; set; } = null!;
    
    public TransferStatus Status { get; set; } = TransferStatus.Pending;
    public string? Notes { get; set; }
    
    // Navigation properties
    public ICollection<StockTransferItem> TransferItems { get; set; } = new List<StockTransferItem>();
}

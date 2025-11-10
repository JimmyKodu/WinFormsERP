namespace WinFormsERP.Core.Entities.Inventory;

/// <summary>
/// Stock Transfer Item entity
/// </summary>
public class StockTransferItem : BaseEntity
{
    public int StockTransferId { get; set; }
    public StockTransfer StockTransfer { get; set; } = null!;
    
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public decimal Quantity { get; set; }
}

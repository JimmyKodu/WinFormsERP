namespace WinFormsERP.Core.Entities.Purchasing;

/// <summary>
/// Supplier entity for purchasing management
/// </summary>
public class Supplier : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? TaxId { get; set; }
    public int PaymentTermsDays { get; set; } = 30;
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}

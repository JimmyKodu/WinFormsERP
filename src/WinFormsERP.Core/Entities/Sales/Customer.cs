namespace WinFormsERP.Core.Entities.Sales;

/// <summary>
/// Customer entity for sales management
/// </summary>
public class Customer : BaseEntity
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
    public decimal CreditLimit { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}

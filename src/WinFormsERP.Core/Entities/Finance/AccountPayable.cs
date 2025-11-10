using WinFormsERP.Core.Enums;
using WinFormsERP.Core.Entities.Purchasing;

namespace WinFormsERP.Core.Entities.Finance;

/// <summary>
/// Account Payable entity for tracking supplier payments
/// </summary>
public class AccountPayable : BaseEntity
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    
    public int? PurchaseOrderId { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
    
    public required string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal BalanceAmount => Amount - PaidAmount;
    public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;
}

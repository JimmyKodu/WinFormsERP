using WinFormsERP.Core.Enums;
using WinFormsERP.Core.Entities.Sales;

namespace WinFormsERP.Core.Entities.Finance;

/// <summary>
/// Account Receivable entity for tracking customer payments
/// </summary>
public class AccountReceivable : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public int? SalesOrderId { get; set; }
    public SalesOrder? SalesOrder { get; set; }
    
    public required string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal BalanceAmount => Amount - PaidAmount;
    public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;
}

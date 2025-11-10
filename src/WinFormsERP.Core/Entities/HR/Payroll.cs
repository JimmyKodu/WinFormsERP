using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.HR;

/// <summary>
/// Payroll entity for employee compensation
/// </summary>
public class Payroll : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public DateTime PayPeriodStart { get; set; }
    public DateTime PayPeriodEnd { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowances { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary => BasicSalary + Allowances - Deductions;
    public PayrollStatus Status { get; set; } = PayrollStatus.Draft;
    public string? Notes { get; set; }
}

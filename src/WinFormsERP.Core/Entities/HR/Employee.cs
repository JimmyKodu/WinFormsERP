using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.HR;

/// <summary>
/// Employee entity for human resources management
/// </summary>
public class Employee : BaseEntity
{
    public required string EmployeeNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime HireDate { get; set; } = DateTime.Today;
    public DateTime? TerminationDate { get; set; }
    public required string Department { get; set; }
    public required string Position { get; set; }
    public decimal Salary { get; set; }
    public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;
    
    // Navigation properties
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}

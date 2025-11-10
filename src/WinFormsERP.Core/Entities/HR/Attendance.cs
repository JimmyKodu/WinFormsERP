using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.HR;

/// <summary>
/// Attendance entity for tracking employee attendance
/// </summary>
public class Attendance : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public DateTime Date { get; set; } = DateTime.Today;
    public TimeSpan? CheckInTime { get; set; }
    public TimeSpan? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string? Notes { get; set; }
    
    public TimeSpan? WorkDuration 
    { 
        get
        {
            if (CheckInTime.HasValue && CheckOutTime.HasValue)
                return CheckOutTime.Value - CheckInTime.Value;
            return null;
        }
    }
}

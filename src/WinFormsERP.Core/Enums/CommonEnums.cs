namespace WinFormsERP.Core.Enums;

public enum OrderStatus
{
    Draft,
    Confirmed,
    Processing,
    Completed,
    Cancelled
}

public enum TransferStatus
{
    Pending,
    InTransit,
    Completed,
    Cancelled
}

public enum AccountType
{
    Asset,
    Liability,
    Equity,
    Revenue,
    Expense
}

public enum JournalEntryStatus
{
    Draft,
    Posted,
    Void
}

public enum PaymentStatus
{
    Unpaid,
    PartiallyPaid,
    Paid,
    Overdue
}

public enum EmploymentStatus
{
    Active,
    OnLeave,
    Suspended,
    Terminated
}

public enum AttendanceStatus
{
    Present,
    Absent,
    Late,
    OnLeave,
    Holiday
}

public enum PayrollStatus
{
    Draft,
    Approved,
    Paid
}

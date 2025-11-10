using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.Finance;

/// <summary>
/// Account entity for chart of accounts
/// </summary>
public class Account : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public AccountType Type { get; set; }
    public int? ParentAccountId { get; set; }
    public Account? ParentAccount { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<Account> SubAccounts { get; set; } = new List<Account>();
    public ICollection<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
}

namespace WinFormsERP.Core.Entities.Finance;

/// <summary>
/// Journal Entry Line entity for double-entry bookkeeping
/// </summary>
public class JournalEntryLine : BaseEntity
{
    public int JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; } = null!;
    
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string? Description { get; set; }
}

using WinFormsERP.Core.Enums;

namespace WinFormsERP.Core.Entities.Finance;

/// <summary>
/// Journal Entry entity for general ledger
/// </summary>
public class JournalEntry : BaseEntity
{
    public required string EntryNumber { get; set; }
    public DateTime EntryDate { get; set; } = DateTime.Today;
    public required string Description { get; set; }
    public JournalEntryStatus Status { get; set; } = JournalEntryStatus.Draft;
    
    // Navigation properties
    public ICollection<JournalEntryLine> EntryLines { get; set; } = new List<JournalEntryLine>();
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
/*
 * This model is design to facilitate item movement from issuer to receiver then back to store
 *  A)  FK => AppUser
 *  B)  FK => Item
 */
[Table("tbltransferhistory")]
public class Transferhistory
{
    [Key] 
    public Guid HistoryId { get; set; }
    public string TransferredFrom { get; set; } = string.Empty;
    public string TransferredTo { get; set; } = string.Empty;

    [ForeignKey("Item")]
    public Guid ItemById { get; set; }
    public Item? Item { get; set; }
    public DateTime DateTransfer { get; set; } = DateTime.Now;
    public string Remarks { get; set; } = string.Empty;

    [ForeignKey("CreatedBy")]
    public string CreatedById { get; set; }
    public AppUser? CreatedBy { get; set; }
}


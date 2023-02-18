using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
/*
 * This model is design to facilitate item movement from issuer to receiver then back to store
 *  A)  FK => AppUser
 *  B)  FK => Item
 */
[Table("tblitememployeeassignment")]
public class ItemEmployeeAssignment
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = new();
    public DateTime DateTaken { get; set; } = DateTime.Now;
    public string IssuerName { get; set; } = string.Empty;
    public string IssueSignature { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public string ReceiverSignature { get; set; } = string.Empty;
    public DateTime? DateReturned { get; set; }
    public string? ReturnedCondition { get; set; }
    public string? ResonForNotReturn { get; set; }
    public bool IsReturned { get; set; }

}


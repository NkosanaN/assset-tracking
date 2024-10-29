using System.ComponentModel.DataAnnotations;
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
	[Key]
	public Guid AssigmentId { get; set; }

	[ForeignKey("IssuerBy")]
	public string IssuerById { get; set; }
	public AppUser? IssuerBy { get; set; }

	[ForeignKey("ReceiverBy")]
	public string ReceiverById { get; set; }
	public AppUser? ReceiverBy { get; set; }

	public Guid ItemId { get; set; }
	public Item? Item { get; set; }
	public DateTime DateTaken { get; set; }
	public string IssueSignature { get; set; } = string.Empty;
	public string ReceiverSignature { get; set; } = string.Empty;
	public DateTime? DateReturned { get; set; }
	public string? Condition { get; set; }
	public string? ReasonForNotReturn { get; set; }
	public bool IsReturned { get; set; }

}


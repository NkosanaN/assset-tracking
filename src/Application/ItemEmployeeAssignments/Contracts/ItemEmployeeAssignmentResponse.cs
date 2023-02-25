using Domain;

namespace Application.ItemEmployeeAssignments.Contracts;

public class ItemEmployeeAssignmentResponse
{
    public Guid ItemEmployeeCode { get; set; }
    public AppUser? IssuerBy { get; set; }
    public AppUser? ReceiverBy { get; set; }
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    public DateTime DateTaken { get; set; } = DateTime.Now;
    public string IssueSignature { get; set; } = string.Empty;
    public string ReceiverSignature { get; set; } = string.Empty;
    public DateTime? DateReturned { get; set; }
    public string? ReturnedCondition { get; set; }
    public string? ResonForNotReturn { get; set; }
    public bool IsReturned { get; set; } = false;

}




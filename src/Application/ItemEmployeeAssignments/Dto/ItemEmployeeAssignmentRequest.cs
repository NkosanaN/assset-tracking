namespace Application.ItemEmployeeAssignments.Dto;

public class ItemEmployeeAssignmentRequest
{
    public string IssuerById { get; set; } = string.Empty;
    public string ReceiverById { get; set; } = string.Empty;
    public Guid ItemId { get; set; }
    public DateTime DateTaken { get; set; } 
    public string IssueSignature { get; set; } = string.Empty;
    public string ReceiverSignature { get; set; } = string.Empty;
    public DateTime? DateReturned { get; set; }
    public string? Condition { get; set; }
    public string? ReasonForNotReturn { get; set; }
    public bool IsReturned { get; set; } = false;
}
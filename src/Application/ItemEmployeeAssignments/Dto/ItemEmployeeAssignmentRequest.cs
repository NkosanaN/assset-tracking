namespace Application.ItemEmployeeAssignments.Dto;

public record ItemEmployeeAssignmentRequest(
    string IssuerById, 
    string ReceiverById, 
    Guid ItemId,
    DateTime DateTaken, 
    string IssueSignature,
    string ReceiverSignature, 
    DateTime? DateReturned,
    string? Condition, 
    string? ReasonForNotReturn,
    bool IsReturned);

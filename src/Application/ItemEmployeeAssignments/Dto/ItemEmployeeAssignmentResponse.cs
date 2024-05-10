using Domain;

namespace Application.ItemEmployeeAssignments.Dto;

public record ItemEmployeeAssignmentResponse(
    Guid AssigmentId,
    Guid ItemEmployeeCode,
    AppUser? IssuerBy, 
    AppUser? ReceiverBy,
    Guid ItemId,
    DateTime DateTaken,
    string IssueSignature,
    string ReceiverSignature, 
    DateTime? DateReturned,
    string? ReturnedCondition,
    string? ResonForNotReturn, 
    bool IsReturned);




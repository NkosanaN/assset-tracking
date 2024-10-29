using Domain;
using FluentValidation;

namespace Application.ItemEmployeeAssignments;

public class ItemEmpAssignmentValidator : AbstractValidator<ItemEmployeeAssignment>
{
	public ItemEmpAssignmentValidator()
	{
		//RuleFor(x => x.AssigmentId).NotEmpty();
		RuleFor(x => x.IssuerById).NotEmpty();
		RuleFor(x => x.ReceiverById).NotEmpty();
		RuleFor(x => x.ItemId).NotEmpty();
		//RuleFor(x => x.IssueSignature).NotEmpty();
		//RuleFor(x => x.ReceiverSignature).NotEmpty();
		//RuleFor(x => x.Condition).NotEmpty();
	}
}

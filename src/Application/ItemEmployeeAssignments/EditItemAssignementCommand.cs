using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemEmployeeAssignments;

/*
* Command don't return any thing
*/
public class EditItemAssignementCommand : IRequest<Result<Unit>>
{
	public ItemEmployeeAssignment ItemEmployeeAssignment { get; set; } = new();
}

public class CommandValidator : AbstractValidator<EditItemAssignementCommand>
{
	public CommandValidator()
	{
		RuleFor(x => x.ItemEmployeeAssignment).SetValidator(new ItemEmpAssignmentValidator());
	}
}

public class EditItemAssignementHandler : IRequestHandler<EditItemAssignementCommand, Result<Unit>>
{
	private readonly IDataContext _context;
	public EditItemAssignementHandler(IDataContext context)
	{
		_context = context;
	}
	public async Task<Result<Unit>> Handle(EditItemAssignementCommand request, CancellationToken cancellationToken)
	{
		//this mapping doesn't work yet  
		//will map this manual 
		var itemtransfer = await GetItemEmployeeAssignmentByIdAsync(request.ItemEmployeeAssignment.AssigmentId);

		if (itemtransfer is null)
		{
			return null!;
		}

		itemtransfer.ItemId = request.ItemEmployeeAssignment.ItemId;
		itemtransfer.IssueSignature = request.ItemEmployeeAssignment.IssueSignature;
		itemtransfer.IssuerById = request.ItemEmployeeAssignment.IssuerById;
		itemtransfer.AssigmentId = request.ItemEmployeeAssignment.AssigmentId;
		itemtransfer.ReceiverById = request.ItemEmployeeAssignment.ReceiverById;
		itemtransfer.ReceiverSignature = request.ItemEmployeeAssignment.ReceiverSignature;
		itemtransfer.DateReturned = /*request.ItemEmployeeAssignment.DateReturned*/ DateTime.Now;
		itemtransfer.Condition = request.ItemEmployeeAssignment.Condition;
		itemtransfer.ReasonForNotReturn = request.ItemEmployeeAssignment.ReasonForNotReturn;
		itemtransfer.IsReturned = request.ItemEmployeeAssignment.IsReturned;

		_context.ItemEmployeeAssignments.Update(itemtransfer);

		int result = await _context.SaveChangeAsync(cancellationToken);

		if (result > 0)
		{
			return Result<Unit>.Failure("Failed to update Item Employee Assignment");
		}

		return Result<Unit>.Success(Unit.Value);
	}

	private async Task<ItemEmployeeAssignment> GetItemEmployeeAssignmentByIdAsync(Guid id)
	{
		return await _context.ItemEmployeeAssignments
				.Include(c => c.Item)
				.Include(x => x.IssuerBy)
				.Include(x => x.ReceiverBy)
				.FirstAsync(c => c.AssigmentId == id);
	}

	#region mapEmployeeAssignment
	//private void mapEmployeeAssignment(ItemEmployeeAssignment itemtransfer, Command request)
	//{
	//    itemtransfer.ItemId = request.ItemEmployeeAssignment.ItemId;
	//    itemtransfer.IsReturned = request.ItemEmployeeAssignment.IsReturned;
	//    itemtransfer.IssueSignature = request.ItemEmployeeAssignment.IssueSignature;
	//    //itemtransfer.IssuerBy = request.ItemEmployeeAssignment.IssuerBy;
	//    //itemtransfer.IssuerById = request.ItemEmployeeAssignment.IssuerById;
	//    itemtransfer.ItemEmployeeCode = request.ItemEmployeeAssignment.ItemEmployeeCode;
	//    //itemtransfer.ReceiverBy = request.ItemEmployeeAssignment.ReceiverBy;
	//    //itemtransfer.ReceiverById = request.ItemEmployeeAssignment.ReceiverById;
	//    itemtransfer.ReturnedCondition = request.ItemEmployeeAssignment.ReturnedCondition;
	//    itemtransfer.ReceiverSignature = request.ItemEmployeeAssignment.ReceiverSignature;
	//    itemtransfer.ResonForNotReturn = request.ItemEmployeeAssignment.ResonForNotReturn;
	//    itemtransfer.DateReturned = request.ItemEmployeeAssignment.DateReturned;

	//}


	#endregion

}


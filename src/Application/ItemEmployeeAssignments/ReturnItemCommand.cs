using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemEmployeeAssignments;

/*
* Command don't return any thing
*/
public class ReturnItemCommand : IRequest<Result<Unit>>
{
	public ItemEmployeeAssignment? IItem { get; set; }

	//public class CommandValidator : AbstractValidator<ReturnItemCommand>
	//{
	//    //public CommandValidator()
	//    //{
	//    //    RuleFor(x => x.Item).SetValidator(new ItemValidator());
	//    //}
	//}
}


public class ReturnItemHandler : IRequestHandler<ReturnItemCommand, Result<Unit>>
{
	private readonly IDataContext _context;

	public ReturnItemHandler(IDataContext context)
	{
		_context = context;
	}

	public async Task<Result<Unit>> Handle(ReturnItemCommand request, CancellationToken cancellationToken)
	{
		var isItemExist = await GetItemEmployeeAssignmentByIdAsync(request.IItem!.AssigmentId);

		if (isItemExist is null)
		{
			return null!;
		}

		bool result = await ReturnItemEmployeeAssignmentAsync(request.IItem.AssigmentId, request.IItem.Condition!);

		if (!result)
		{
			return Result<Unit>.Failure("Fail to create booking");
		}

		//Unit.Value is the same as return nothing as Command don't return anything

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

	private async Task<bool> ReturnItemEmployeeAssignmentAsync(Guid id, string note)
	{
		var itemTransfer = await _context.ItemEmployeeAssignments.FindAsync(id);
		int result = 0;

		if (itemTransfer is null)
		{
			itemTransfer!.Condition = note;
			itemTransfer.IsReturned = true;
			itemTransfer.DateReturned = DateTime.Now;

			result = await _context.SaveChangeAsync(default);
		};

		return result == 0;
	}
}

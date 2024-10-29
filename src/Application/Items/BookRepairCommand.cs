using Application.Core;
using MediatR;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Persistence;

namespace Application.Items;

/*
* Command don't return any thing
*/
public class BookRepairCommand : IRequest<Result<Unit>>
{
	public Item? Item { get; set; }
	//public Guid Id { get; set; }
	//public string Note { get; set; }

}

public class CommandValidator : AbstractValidator<BookRepairCommand>
{
	//public CommandValidator()
	//{
	//    RuleFor(x => x.Item).SetValidator(new ItemValidator());
	//}
}

public class BookRepairHandler : IRequestHandler<BookRepairCommand, Result<Unit>>
{
	private readonly IDataContext _context;

	public BookRepairHandler(IDataContext context)
	{
		_context = context;
	}

	public async Task<Result<Unit>> Handle(BookRepairCommand request, CancellationToken cancellationToken)
	{
		bool data = await BookRepairAsync(request.Item!.ItemId);

		if (!data)
		{
			return Result<Unit>.Failure("Fail to create booking");
		}

		//Unit.Value is the same as return nothing as Command don't return anything

		return Result<Unit>.Success(Unit.Value);
	}

	public async Task<bool> BookRepairAsync(Guid id)
	{
		var rowsModified = await _context.Items.SingleAsync(item => item.ItemId == id);

		if (rowsModified is not null)
		{
			rowsModified.DueforRepair = true;
			_context.Items.Update(rowsModified);
			await _context.SaveChangeAsync(default);
		}

		return true;
	}
}


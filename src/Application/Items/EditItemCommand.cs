using Application.Core;
using MediatR;
using Domain;
//using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Persistence;

namespace Application.Items;

/*
* Command don't return any thing
*/
public class EditItemCommand : IRequest<Result<Unit>>
{
	public Item? Item { get; set; }
}
//git remote set-url origin https://github.com/NkosanaN/assset-tracking.git			
//public class CommandValidator : AbstractValidator<EditItemCommand>
//{
//    public CommandValidator()
//    {
//        RuleFor(x => x.Item).SetValidator(new ItemValidator());
//    }
//}

public class EditItemHandler(IDataContext context) : IRequestHandler<EditItemCommand, Result<Unit>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<Unit>> Handle(EditItemCommand request, CancellationToken cancellationToken)
	{
		var isItemExist = await GetItemWithShelveByIdAsync(request.Item!.ItemId);

		if (isItemExist is null)
		{
			return null!;
		}

		var item = new Item
		{
			ItemId = isItemExist.ItemId,
			Name = request.Item.Name,
			Description = request.Item.Description,
			Serialno = request.Item.Serialno,
			ItemTag = "Empty",
			Cost = request.Item.Cost,
			Qty = request.Item.Qty,
			DatePurchased = request.Item.DatePurchased,
			DueforRepair = isItemExist.DueforRepair,
			ShelfId = request.Item.ShelfId,
			ShelveBy = new ShelveType { ShelfId = request.Item.ShelfId },
			CreatedBy = isItemExist.CreatedBy,
			CreatedById = isItemExist.CreatedById
		};

		_context.Items.Update(item);

		int result = await _context.SaveChangeAsync(cancellationToken);

		if (result > 0)
		{
			return Result<Unit>.Failure("Failed to update item");
		}

		return Result<Unit>.Success(Unit.Value);

	}

	private async Task<Item> GetItemWithShelveByIdAsync(Guid id)
	{
		return await _context.Items.AsNoTracking().Include(x => x.ShelveBy).SingleAsync(c => c.ItemId == id);
	}
}

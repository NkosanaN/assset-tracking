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
public class CreateItemCommand : IRequest<Result<Unit>>
{
	public Item Item { get; set; } = new();
}

//public class CommandValidator : AbstractValidator<CreateItemCommand>
//{
//    public CommandValidator()
//    {
//        RuleFor(x => x.Item).SetValidator(new ItemValidator());
//    }
//}

public class CreateItemHandler : IRequestHandler<CreateItemCommand, Result<Unit>>
{
	private readonly IDataContext _context;

	public CreateItemHandler(IDataContext context)
	{
		_context = context;
	}

	public async Task<Result<Unit>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
	{
		#region
		//var user = await _context.Users
		//    .FirstOrDefaultAsync(x =>
		//        x.UserName == _userAccessor.GetUsername());

		////will remove this part just for test now!!
		//var itemtransfer = new ItemTranfer
		//{
		//    AppUser = user!,
		//    Item = request.Item,
		//    InOut = true
		//};
		////will remove this part just for test now!!
		//request.Item.ItemsTrackings.Add(itemtransfer);
		#endregion

		var exist = await _context.Items.SingleAsync(item => item.Name == request.Item.Name, cancellationToken: cancellationToken);

		if (exist is null)
		{
			return Result<Unit>.Failure("Item Name already exist");
		}

		var item = new Item
		{
			ItemId = Guid.NewGuid(),
			Name = request.Item.Name,
			Description = request.Item.Description,
			Serialno = request.Item.Serialno,
			ItemTag = request.Item.ItemTag,
			Cost = request.Item.Cost,
			Qty = request.Item.Qty,
			DatePurchased = request.Item.DatePurchased,
			DueforRepair = false,
			ShelfId = request.Item.ShelfId,
			CreatedById = request.Item.CreatedById
		};

		await _context.Items.AddAsync(item, cancellationToken);

		int data = await _context.SaveChangeAsync(cancellationToken);

		if (data > 0)
		{
			return Result<Unit>.Failure("Fail to create Item");
		}

		//Unit.Value is the same as return nothing as Command don't return anything

		return Result<Unit>.Success(Unit.Value);
	}

}

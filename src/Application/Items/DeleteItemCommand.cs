using Application.Contracts.Persistence;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.Items;

/*
 * Command don't return any thing
 */
public class DeleteItemCommand : IRequest<Result<Unit>>
{
	public Guid Id { get; set; }
}

public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Result<Unit>>
{
	private readonly IDataContext _context;

	public DeleteItemHandler(IDataContext context)
	{
		_context = context;
	}

	public async Task<Result<Unit>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
	{
		var item = await _context.Items.SingleAsync(item => item.ItemId == request.Id, cancellationToken: cancellationToken);

		if (item is null)
		{
			return null!;
		}

		_context.Items.Remove(item);

		int result = await _context.SaveChangeAsync(cancellationToken);

		if (result > 0)
		{
			return Result<Unit>.Failure("Failed to delete the Item");
		}

		//Unit.Value is the same as return nothing as Command don't return anything
		return Result<Unit>.Success(Unit.Value);
	}
}

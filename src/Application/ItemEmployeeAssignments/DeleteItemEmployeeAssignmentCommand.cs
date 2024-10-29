using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.ItemEmployeeAssignments;

/*
* Command don't return any thing
*/
public class DeleteItemEmployeeAssignmentCommand : IRequest<Result<Unit>>
{
	public Guid Id { get; set; }
}

public class DeleteItemEmployeeAssignmentHandler : IRequestHandler<DeleteItemEmployeeAssignmentCommand, Result<Unit>>
{
	private readonly IDataContext _context;

	public DeleteItemEmployeeAssignmentHandler(IDataContext context)
	{
		_context = context;
	}

	public async Task<Result<Unit>> Handle(DeleteItemEmployeeAssignmentCommand request, CancellationToken cancellationToken)
	{
		var item = await _context.ItemEmployeeAssignments.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

		if (item is null)
		{
			return null!;
		}

		_context.ItemEmployeeAssignments.Remove(item);

		int result = await _context.SaveChangeAsync(cancellationToken);

		if (result > 0)
		{
			return Result<Unit>.Failure("Fail to Delete Item Employee Assignment");
		}

		//Unit.Value is the same as return nothing as Command don't return anything
		return Result<Unit>.Success(Unit.Value);
	}
}

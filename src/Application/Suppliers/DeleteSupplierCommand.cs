using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Suppliers;

public class DeleteSupplierCommand : IRequest<Result<Unit>>
{
	public Guid Id { get; set; }
}

public class DeleteSupplierCommandHandler(IDataContext context)
	: IRequestHandler<DeleteSupplierCommand, Result<Unit>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<Unit>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
	{
		var sup = await _context.Suppliers.FindAsync([request.Id], cancellationToken: cancellationToken);

		if (sup is null)
		{
			return null!;
		}

		var result = _context.Suppliers.Remove(sup);

		if (result is not null)
		{
			return Result<Unit>.Failure("Failed to delete the Supplier");
		}

		return Result<Unit>.Success(Unit.Value);
	}
}

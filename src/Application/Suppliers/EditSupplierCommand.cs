using Application.Contracts.Persistence;
using Application.Core;
//using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Suppliers;

/*
* Command don't return any thing
*/
public class EditSupplierCommand : IRequest<Result<Unit>>
{
	public Domain.Supplier? Supplier { get; set; }
}
public class DeleteSupplierHandler(IDataContext context) : IRequestHandler<EditSupplierCommand, Result<Unit>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<Unit>> Handle(EditSupplierCommand request, CancellationToken cancellationToken)
	{
		var sup = await _context.Suppliers.FirstAsync(supplier => supplier.SupplierId == request.Supplier!.SupplierId, cancellationToken: cancellationToken);

		if (sup is null)
		{
			return null!;
		}

		sup.SupplierName = request.Supplier!.SupplierName;
		sup.SupplierDescription = request.Supplier.SupplierDescription;

		_context.Suppliers.Update(sup);

		int result = await _context.SaveChangeAsync(cancellationToken);

		if (result > 0)
		{
			return Result<Unit>.Failure("Failed to update Supplier");
		}

		//Unit.Value is the same as return nothing as Command don't return anything
		return Result<Unit>.Success(Unit.Value);
	}
}

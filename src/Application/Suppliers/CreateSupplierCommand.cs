using Application.Contracts.Persistence;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Application.Suppliers;

/*
* Command don't return any thing
*/
public class CreateSupplierCommand : IRequest<Result<Unit>>
{
	public SupplierDto SupplierDto { get; set; } = new();
}

public class CommandValidator : AbstractValidator<CreateSupplierCommand>
{
	public CommandValidator()
	{
		RuleFor(x => x.SupplierDto).SetValidator(new SupplierValidator());
	}
}

public class CreateSupplierCommandHandler(IDataContext context)
	: IRequestHandler<CreateSupplierCommand, Result<Unit>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<Unit>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
	{
		bool exist = await _context.Suppliers.AnyAsync(c => c.SupplierName == request.SupplierDto.SupplierName,
			cancellationToken: cancellationToken);

		if (exist)
		{
			return Result<Unit>.Failure("Supplier Name already exist. Please use another name");
		}

		var model = new Domain.Supplier
		{
			SupplierId = Guid.NewGuid(),
			SupplierName = request.SupplierDto.SupplierName,
			SupplierDescription = request.SupplierDto.SupplierDescription,
		};

		await _context.Suppliers.AddAsync(model, cancellationToken);

		int result = await _context.SaveChangeAsync(cancellationToken);

		if (result == 0)
		{
			return Result<Unit>.Failure("Fail to create Supplier");
		}

		return Result<Unit>.Success(Unit.Value);
	}


}

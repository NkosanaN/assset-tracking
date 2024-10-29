using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Suppliers;

public class GetSupplierDetailQuery : IRequest<Result<Domain.Supplier>>
{
	public Guid SupplierId { get; set; }
}

public class GetSupplierDetailHandler(IDataContext context)
	: IRequestHandler<GetSupplierDetailQuery, Result<Domain.Supplier>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<Domain.Supplier>> Handle(GetSupplierDetailQuery request, CancellationToken cancellationToken)
	{
		var query = await _context.Suppliers.FindAsync([request.SupplierId], cancellationToken: cancellationToken);

		return Result<Domain.Supplier>.Success(query);
	}
}

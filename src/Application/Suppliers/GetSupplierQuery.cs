using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Suppliers;

public class GetSupplierQuery : IRequest<Result<PagedList<Domain.Supplier>>>
{
	public PagingParams? Params { get; set; }
}

public class GetSupplierQueryHandler(IDataContext context)
	: IRequestHandler<GetSupplierQuery, Result<PagedList<Domain.Supplier>>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<PagedList<Domain.Supplier>>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
	{
		return Result<PagedList<Domain.Supplier>>.Success(
				await PagedList<Domain.Supplier>.CreateAsync(_context.Suppliers.AsQueryable(), request.Params!.PageNumber,
						request.Params.PageSize));
	}
}

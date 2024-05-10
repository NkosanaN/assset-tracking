using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Suppliers;

public class List
{
    public class Query : IRequest<Result<PagedList<Domain.Supplier>>>
    {
        public PagingParams? Params { get; set; }
    }

    public class Handler(ISupplierRepository context) : IRequestHandler<Query, Result<PagedList<Domain.Supplier>>>
    {
        private readonly ISupplierRepository _context = context;

        public async Task<Result<PagedList<Domain.Supplier>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetAllSupplier();

            return Result<PagedList<Domain.Supplier>>.Success(
                await PagedList<Domain.Supplier>.CreateAsync(query, request.Params!.PageNumber,
                    request.Params.PageSize));
        }
    }
}

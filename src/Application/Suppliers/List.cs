using Application.Core;
using MediatR;
using Persistence;

namespace Application.Suppliers;

public class List
{
    public class Query : IRequest<Result<PagedList<Domain.Supplier>>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<Domain.Supplier>>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<Domain.Supplier>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Suppliers.AsQueryable();

            return Result<PagedList<Domain.Supplier>>.Success(
                await PagedList<Domain.Supplier>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize));
        }
    }
}

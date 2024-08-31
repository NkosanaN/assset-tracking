using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Suppliers;

public class Details
{
    public class Query : IRequest<Result<Domain.Supplier>>
    {
        public Guid SupplierId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Domain.Supplier>>
    {
        private readonly ISupplierRepository _context;

        public Handler(ISupplierRepository context)
        {
            _context = context;
        }

        public async Task<Result<Domain.Supplier>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetByIdAsync(request.SupplierId);

            return Result<Domain.Supplier>.Success(query);
        }
    }
}
using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using MediatR;

namespace Application.ShelveTypes;

public class Details
{
    public class Query : IRequest<Result<ShelveType>>
    {
        public Guid ShelfId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ShelveType>>
    {
        private readonly IShelveRepository _context;

        public Handler(IShelveRepository context)
        {
            _context = context;
        }

        public async Task<Result<ShelveType>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetByIdAsync(request.ShelfId);

            return Result<ShelveType>.Success(query);
        }
    }
}

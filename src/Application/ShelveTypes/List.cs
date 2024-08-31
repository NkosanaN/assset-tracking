using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using MediatR;

namespace Application.ShelveTypes;

public class List
{
    public class Query : IRequest<Result<PagedList<ShelveType>>>
    {
        public PagingParams? Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ShelveType>>>
    {
        private readonly IShelveRepository _context;

        public Handler(IShelveRepository context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<ShelveType>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetAllShelveType();

            return Result<PagedList<ShelveType>>.Success(
                await PagedList<ShelveType>.CreateAsync(query, request.Params!.PageNumber,
                    request.Params.PageSize));
        }
    }
}

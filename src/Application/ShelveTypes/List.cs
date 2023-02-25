using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.ShelveTypes;

public class List
{
    public class Query : IRequest<Result<PagedList<ShelveType>>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ShelveType>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ShelveType>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.ShelveTypes.AsQueryable();

            return Result<PagedList<ShelveType>>.Success(
                await PagedList<ShelveType>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize));
        }
    }
}

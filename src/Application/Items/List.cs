using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Items;

public class List
{
    public class Query : IRequest<Result<PagedList<ItemDto>>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ItemDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ItemDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return Result<PagedList<ItemDto>>.Success(
                await PagedList<ItemDto>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize));
        }

    }

}

using Application.Contracts.Persistence;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items;

public class List
{
    public class Query : IRequest<Result<PagedList<ItemDto>>>
    {
        public PagingParams? Params { get; set; }
    }

    public class Handler(IItemRepository context, IMapper mapper) : IRequestHandler<Query, Result<PagedList<ItemDto>>>
    {
        private readonly IItemRepository _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<PagedList<ItemDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetAllItem();

            var list = query
                .AsNoTracking()
               // .Include(c=>c.ShelveBy)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider).AsQueryable();
            
            return Result<PagedList<ItemDto>>.Success(
                await PagedList<ItemDto>.CreateAsync(list, request.Params!.PageNumber,
                    request.Params.PageSize));
        }

    }

}

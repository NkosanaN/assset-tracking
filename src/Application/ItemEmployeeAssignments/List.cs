using Application.Core;
using Application.ItemEmployeeAssignments.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ItemEmployeeAssignments;

public class List
{
    public class Query : IRequest<Result<PagedList<ItemEmployeeAssignmentResponse>>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ItemEmployeeAssignmentResponse>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ItemEmployeeAssignmentResponse>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            
            var query = _context.ItemEmployeeAssignments
                .Include(c => c.Item)
                .Include(x=>x.IssuerBy)
                .Include(x=>x.ReceiverBy)
                .ProjectTo<ItemEmployeeAssignmentResponse>(_mapper.ConfigurationProvider).AsQueryable();
            
            return Result<PagedList<ItemEmployeeAssignmentResponse>>.Success(
                await PagedList<ItemEmployeeAssignmentResponse>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize));
        }

    }
}

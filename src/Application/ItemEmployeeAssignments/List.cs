using Application.Contracts.Persistence;
using Application.Core;
using Application.ItemEmployeeAssignments.Dto;
using Application.Items;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemEmployeeAssignments;

public class List
{
    public class Query : IRequest<Result<PagedList<ItemEmployeeAssignmentResponse>>>
    {
        public PagingParams? Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ItemEmployeeAssignmentResponse>>>
    {
        private readonly IItemEmployeeAssignmentRepository _context;
        private readonly IMapper _mapper;

        public Handler(IItemEmployeeAssignmentRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ItemEmployeeAssignmentResponse>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var query = await _context.GetItemEmployeeAssignmentList();

            var list = query
                .AsNoTracking()
                .ProjectTo<ItemEmployeeAssignmentResponse>(_mapper.ConfigurationProvider)
                .AsQueryable();
            
            return Result<PagedList<ItemEmployeeAssignmentResponse>>.Success(
                await PagedList<ItemEmployeeAssignmentResponse>.CreateAsync(list, request.Params!.PageNumber,
                    request.Params.PageSize));
        }

    }
}

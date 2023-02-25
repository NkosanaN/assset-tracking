using Application.Core;
using Application.Departments.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Persistence;

namespace Application.Departments;

public class List
{
    public class Query : IRequest<Result<PagedList<Department>>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<Department>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<Department>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Departments.AsQueryable();

            return Result<PagedList<Department>>.Success(
                await PagedList<Department>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize));
        }
    }
}

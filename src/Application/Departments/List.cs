using Application.Contracts.Persistence;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Departments;

public class List
{
    public class Query : IRequest<Result<PagedList<Department>>>
    {
        public PagingParams? Params { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result<PagedList<Department>>>
    {
        private readonly IDepartmentRepository _context;
        public Handler(IDepartmentRepository context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<Department>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetAllDepartment();

  
            return Result<PagedList<Department>>.Success(
                await PagedList<Department>.CreateAsync(query, request.Params!.PageNumber,
                    request.Params.PageSize));
        }
    }
}

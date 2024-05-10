using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using MediatR;

namespace Application.Departments;
public class Details
{
    public class Query : IRequest<Result<Department>>
    {
        public Guid DepartmentId { get; set; }
    }
    public class Handler(IDepartmentRepository context) : IRequestHandler<Query, Result<Department>>
    {
        private readonly IDepartmentRepository _context = context;

        public async Task<Result<Department>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetByIdAsync(request.DepartmentId);

            return Result<Department>.Success(query);
        }
    }
}

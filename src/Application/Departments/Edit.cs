using Application.Core;
using AutoMapper;
using MediatR;
using Domain;
using Persistence;

namespace Application.Departments;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Department department { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var depart = await _context.Departments.FindAsync(request.department.DepartmentId);

            if (depart is null) return null!;

            depart.DepartmentName = request.department.DepartmentName;
            depart.Description = request.department.Description;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update Department");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);


        }

    }
}
using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Departments;

public class Delete
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(IDepartmentRepository context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IDepartmentRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var depart = await _context.GetByIdAsync(request.Id);

            if (depart is null) return null!;

            var result = await _context.DeleteAsync(depart);

            if (!result) return Result<Unit>.Failure("Failed to delete the Department");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
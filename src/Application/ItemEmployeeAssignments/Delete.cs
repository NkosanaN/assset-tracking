using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.ItemEmployeeAssignments;

public class Delete
{ 
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(IItemEmployeeAssignmentRepository context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IItemEmployeeAssignmentRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var item = await _context.GetByIdAsync(request.Id);

            if (item is null) return null!;
            
            var result = await _context.DeleteAsync(item);

            if (!result) return Result<Unit>.Failure("Fail to Delete Item Employee Assignment");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);
        }
    }
}


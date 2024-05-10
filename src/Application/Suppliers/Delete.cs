using Application.Contracts.Persistence;
using Application.Core;
using MediatR;

namespace Application.Suppliers;
public class Delete
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(ISupplierRepository context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ISupplierRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var sup = await _context.GetByIdAsync(request.Id);

            if (sup is null) return null!;

            var result = await _context.DeleteAsync(sup);

            if (!result) return Result<Unit>.Failure("Failed to delete the Supplier");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
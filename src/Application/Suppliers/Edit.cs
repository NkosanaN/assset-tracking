using Application.Contracts.Persistence;
using Application.Core;
using Application.Supplier;
using FluentValidation;
using MediatR;

namespace Application.Suppliers;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Domain.Supplier? Supplier { get; set; }
    }
    public class Handler(ISupplierRepository context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ISupplierRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var sup = await _context.GetByIdAsync(request.Supplier!.SupplierId);

            if (sup is null) return null!;
            
            sup.SupplierName = request.Supplier.SupplierName;
            sup.SupplierDescription = request.Supplier.SupplierDescription;

            var result = await _context.UpdateAsync(sup);

            if (!result) return Result<Unit>.Failure("Failed to update Supplier");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
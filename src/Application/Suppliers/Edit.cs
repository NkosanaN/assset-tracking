using Application.Core;
using AutoMapper;
using MediatR;
using Domain;
using Persistence;

namespace Application.Suppliers;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Domain.Supplier supplier { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var sup = await _context.Suppliers.FindAsync(request.supplier.SupplierId);

            if (sup is null) return null!;

            sup.SupplierName = request.supplier.SupplierName;
            sup.SupplierDescription = request.supplier.SupplierDescription;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update Supplier");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);


        }

    }
}
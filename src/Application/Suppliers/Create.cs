using Application.Core;
using MediatR;
using FluentValidation;
using Persistence;
using Application.Supplier;
using Application.Suppliers.Contracts;

namespace Application.Suppliers;

public class Create
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public SupplierRequest SupplierRequest { get; set; } = new();
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.SupplierRequest).SetValidator(new SupplierValidator());
        }
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
            var model = new Domain.Supplier
            {
                SupplierId = Guid.NewGuid(),
                SupplierName = request.SupplierRequest.SupplierName,
                SupplierDescription = request.SupplierRequest.SupplierDescription,
            };

            _context.Suppliers.Add(model);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to create Supplier");

            return Result<Unit>.Success(Unit.Value);
        }

    }

}

﻿using Application.Contracts.Persistence;
using Application.Core;
using MediatR;
using FluentValidation;
using Application.Supplier;
namespace Application.Suppliers;

public class Create
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public SupplierDto SupplierDto { get; set; } = new();
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.SupplierDto).SetValidator(new SupplierValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ISupplierRepository _context;

        public Handler(ISupplierRepository context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var exist = await _context.IsSupplierNameUnique(request.SupplierDto.SupplierName) == false;

            if (exist)
            {
                return Result<Unit>.Failure("Supplier Name already exist. Please use another name");
            }

            var model = new Domain.Supplier
            {
                SupplierId = Guid.NewGuid(),
                SupplierName = request.SupplierDto.SupplierName,
                SupplierDescription = request.SupplierDto.SupplierDescription,
            };

            var result = await _context.CreateAsync(model);

            if (!result) return Result<Unit>.Failure("Fail to create Supplier");

            return Result<Unit>.Success(Unit.Value);
        }

    }

}

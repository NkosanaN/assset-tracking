using Application.Core;
using AutoMapper;
using MediatR;
using Domain;
using FluentValidation;
using Application.Contracts.Persistence;

namespace Application.Items;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Item? Item { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Item).SetValidator(new ItemValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IItemRepository _context;
        private readonly IMapper _mapper;

        public Handler(IItemRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
        
            var item = await _context.GetItemWithShelveById(request.Item!.ItemId);

            if (item is null) return null!;

            _mapper.Map(request.Item, item);

            var result = await _context.UpdateAsync(item);

            if (!result) return Result<Unit>.Failure("Failed to update item");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);


        }

    }
}

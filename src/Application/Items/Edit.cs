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
            var isItemExist = await _context.GetItemWithShelveById(request.Item!.ItemId);

            if (isItemExist is null) return null!;

            var item = new Item
            {
                ItemId = isItemExist.ItemId,
                Name = request.Item.Name,
                Description = request.Item.Description,
                Serialno = request.Item.Serialno,
                ItemTag = "Empty",
                Cost = request.Item.Cost,
                Qty = request.Item.Qty,
                DatePurchased = request.Item.DatePurchased,
                DueforRepair = isItemExist.DueforRepair,
                ShelfId = request.Item.ShelfId,
                ShelveBy = new ShelveType { ShelfId = request.Item.ShelfId },
                CreatedBy = isItemExist.CreatedBy,
                CreatedById = isItemExist.CreatedById
            };

            var result = await _context.UpdateAsync(item);

            if (!result) return Result<Unit>.Failure("Failed to update item");
            
            return Result<Unit>.Success(Unit.Value);


        }

    }
}
